USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFromInFactMagasinePartialDeliting]    Script Date: 02.04.2017 19:25:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[DeleteFromInFactMagasinePartialDeliting]
	(
		@StudyId int, 
		@TimeOfVisit datetime, 
		@PatientId int 
	)
AS
BEGIN
	Declare @NecessaryVisitId int; 
	Declare @NecessaryPaymentId int; 
	Declare @NecessaryStudyProcessId int;
	Declare @CountRepet int;

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;
		select @NecessaryVisitId=[Visit].[Id],@NecessaryPaymentId=[Payment].[Id], 
				@NecessaryStudyProcessId=[TheStudyProcess].[Id] 
		--select [Visit].[Id],[Payment].[Id],[TheStudyProcess].[Id] 
		FROM [Payment] 
			Inner Join [Visit] On [Payment].[VisitId]=Visit.[Id] 
			Inner Join [TheStudyProcess] On [Payment].[StudyProcessId]=[TheStudyProcess].[Id] 
			Inner Join Visit B On [TheStudyProcess].[VisitId]=B.[Id] 
		Where (Visit.[PatientId]=@PatientId and B.[PatientId]=@PatientId) and [TheStudyProcess].[StudyId]=@StudyId 
		and ([Visit].[Date]>= convert(datetime,@TimeOfVisit,120) and [Visit].[Date]<dateadd(day,1,convert(datetime,@TimeOfVisit,120)))

		BEGIN TRY
		BEGIN TRANSACTION
			Select @CountRepet=(Select  COUNT ([TheStudyProcessId]) as Counting from [TheStudyProcessLaborants] 
					where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId
						Group by [TheStudyProcessLaborants].[TheStudyProcessId])
			if (@CountRepet=2)
				Begin
					Set ROWCOUNT 1
					Delete From [TheStudyProcessLaborants] Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId
					Set ROWCOUNT 0
					Update [Payment] Set [Payment].[FinalCost]=Null, [Payment].[NoteForDiscount]=Null, [Payment].[IsCash]=0 Where [Payment].[Id]=@NecessaryPaymentId 
					Update [TheStudyProcessLaborants] Set [TheStudyProcessLaborants].[LaborantId]=Null Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId			
					Update [TheStudyProcess] Set [TheStudyProcess].[DoctorData]=Null Where [TheStudyProcess].[Id]=@NecessaryStudyProcessId 
					Update [Visit] Set [Visit].[IsNeedSendEmail]=0,[Visit].[IsHasVisited]=0 Where [Visit].[Id]=@NecessaryVisitId
				End 
			else 
				Begin
					Update [Payment] Set [Payment].[FinalCost]=Null, [Payment].[NoteForDiscount]=Null, [Payment].[IsCash]=0 Where [Payment].[Id]=@NecessaryPaymentId 
					Update [TheStudyProcessLaborants] Set [TheStudyProcessLaborants].[LaborantId]=Null Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId			
					Update [TheStudyProcess] Set [TheStudyProcess].[DoctorData]=Null Where [TheStudyProcess].[Id]=@NecessaryStudyProcessId 
					Update [Visit] Set [Visit].[IsNeedSendEmail]=0,[Visit].[IsHasVisited]=0 Where [Visit].[Id]=@NecessaryVisitId 
				End
		COMMIT;
		END TRY

		BEGIN CATCH
			ROLLBACK TRAN 
		END CATCH
   
END
