USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[UpdateDataOfPatientInPreorderMagasine]    Script Date: 20.02.2017 13:22:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[UpdateDataOfPatientInPreorderMagasine]
	    (
			@PatientId int, 
			@OldStudyId int, 
			@OldTimeOfVisit datetime, 

			@NewTimeOfVisit datetime, 
			@NewStudyId int 
		) 
	AS 
	BEGIN 
			SET NOCOUNT OFF; 
			Declare @NecessaryVisitId int; 
			Declare @NecessaryPaymentId int; 
			Declare @NecessaryStudyProcessId int; 

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


		BEGIN TRY 
		BEGIN TRANSACTION 
			Update [Visit] Set [Visit].[Date]=@NewTimeOfVisit where [Visit].[Id]=@NecessaryVisitId 
			Update [TheStudyProcess] Set [TheStudyProcess].[StudyId]=@NewStudyId where 
			[TheStudyProcess].[Id]=@NecessaryStudyProcessId 
		COMMIT; 
		END TRY 

	BEGIN CATCH 
		ROLLBACK TRAN 
	END CATCH 

END