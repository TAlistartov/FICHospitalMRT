USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[UpdateDataOfPatientInInFactMagasine]    Script Date: 30.04.2017 13:41:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[UpdateDataOfPatientInInFactMagasine]
	(
			@PatientId int, 
			@OldStudyId int, 
			@NewStudyId int, 
			@OldTimeOfVisit datetime, 	
			@IsNeedSendEmail bit,
			@IsCash bit,
			@LaborantId1 int,
			@NewLaborantId1 int=Null,
			@LaborantId2 int=Null,
			@NewLaborantId2 int=Null,
			@FinalCost numeric(6,2),
			@NoteForDiscount nvarchar(100)=Null,
			@DoctorData nvarchar(30)=Null
					
			
	)
AS
BEGIN
	SET NOCOUNT OFF; 
			Declare @NecessaryVisitId int; 
			Declare @NecessaryPaymentId int; 
			Declare @NecessaryStudyProcessId int; 
			Declare @CountRepet int;

			select @NecessaryVisitId=[Visit].[Id],@NecessaryPaymentId=[Payment].[Id], 
			@NecessaryStudyProcessId=[TheStudyProcess].[Id] 
			--select [Visit].[Id],[Payment].[Id],[TheStudyProcess].[Id] 
			FROM [Payment] 
				Inner Join [Visit] On [Payment].[VisitId]=Visit.[Id] 
				Inner Join [TheStudyProcess] On [Payment].[StudyProcessId]=[TheStudyProcess].[Id] 
				Inner Join Visit B On [TheStudyProcess].[VisitId]=B.[Id] 
			Where (Visit.[PatientId]=@PatientId and B.[PatientId]=@PatientId) and [TheStudyProcess].[StudyId]=@OldStudyId 
					and ([Visit].[Date]>= convert(datetime,@OldTimeOfVisit,120) and 
					[Visit].[Date]<dateadd(day,1,convert(datetime,@OldTimeOfVisit,120))) 

			Select @CountRepet=(Select  COUNT ([TheStudyProcessId]) as Counting from [TheStudyProcessLaborants] 
					where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId
						Group by [TheStudyProcessLaborants].[TheStudyProcessId])

		BEGIN TRY 
		BEGIN TRANSACTION 
			Update [Visit] Set [Visit].[IsNeedSendEmail]=@IsNeedSendEmail, [Visit].[IsHasVisited]=1 where [Visit].[Id]=@NecessaryVisitId 
			Update [Payment] Set [Payment].[IsCash]=@IsCash, [Payment].[NoteForDiscount]=@NoteForDiscount,
							     [Payment].[FinalCost]=@FinalCost where [Payment].[Id]=@NecessaryPaymentId
			Update [TheStudyProcess] Set [TheStudyProcess].[DoctorData]=@DoctorData, [TheStudyProcess].[StudyId]=@NewStudyId
					where [TheStudyProcess].[Id]=@NecessaryStudyProcessId 

			if (@CountRepet=2)
				Begin
					if(@NewLaborantId2 is not null)
						begin 
							
							Update [TheStudyProcessLaborants] Set [TheStudyProcessLaborants].[LaborantId]=@NewLaborantId1 
									Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId and 
											[TheStudyProcessLaborants].[LaborantId]=@LaborantId1
							
							Update [TheStudyProcessLaborants] Set [TheStudyProcessLaborants].[LaborantId]=@NewLaborantId2 
									Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId and
											[TheStudyProcessLaborants].[LaborantId]=@LaborantId2
							
						end
					 else
						begin
							Set ROWCOUNT 1
							Delete From [TheStudyProcessLaborants] Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId
							Set ROWCOUNT 0
							Update [TheStudyProcessLaborants] Set [TheStudyProcessLaborants].[LaborantId]=@NewLaborantId1 
									Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId
						End
				End
			else
				Begin
					if(@NewLaborantId2 is not null)
						begin 
							Update [TheStudyProcessLaborants] Set [TheStudyProcessLaborants].[LaborantId]=@NewLaborantId1 
									Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId
														
							Insert Into  [TheStudyProcessLaborants] ([TheStudyProcessLaborants].[TheStudyProcessId],[TheStudyProcessLaborants].[LaborantId]) 
										Values (@NecessaryStudyProcessId,@NewLaborantId2)
									
						end
					else
						begin
							Update [TheStudyProcessLaborants] Set [TheStudyProcessLaborants].[LaborantId]=@NewLaborantId1 
									Where [TheStudyProcessLaborants].[TheStudyProcessId]=@NecessaryStudyProcessId
						end
				End



		COMMIT; 
		END TRY 

	BEGIN CATCH 
		ROLLBACK TRAN 
	END CATCH 
END
