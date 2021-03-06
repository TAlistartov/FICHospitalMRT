USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[DeletePatientFromPreorderMagasine]    Script Date: 11.03.2017 20:49:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[DeletePatientFromPreorderMagasine]
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
			Delete From [Payment] Where [Payment].[Id]=@NecessaryPaymentId 
			Delete From [TheStudyProcessLaborants] Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId
			Delete From [TheStudyProcess] Where [TheStudyProcess].[Id]=@NecessaryStudyProcessId 
			Delete From [Visit] Where [Visit].[Id]=@NecessaryVisitId 
		COMMIT;
		END TRY

		BEGIN CATCH
			ROLLBACK TRAN 
		END CATCH
			

END
