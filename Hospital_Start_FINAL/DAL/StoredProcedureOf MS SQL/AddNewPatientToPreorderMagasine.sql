USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[AddNewPatientToPreorderMagasine]    Script Date: 11.03.2017 20:09:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AddNewPatientToPreorderMagasine]
(
	@PatientId int,
	@StudyId int,
	@DateAndTimeOfPreordering datetime,
	@IsPreorder bit,
	@IsNeedSendEmail bit,
	@IsHasVisited bit,
	@IsCash bit
)
AS
BEGIN

	BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT OFF;

 BEGIN TRANSACTION
    INSERT INTO [Visit] ([Visit].[Date],[Visit].[IsPreorder],[Visit].[PatientId],[Visit].[IsNeedSendEmail],[Visit].[IsHasVisited])
  VALUES(@DateAndTimeOfPreordering,@IsPreorder,@PatientId,@IsNeedSendEmail,@IsHasVisited);
 
 declare @IdOfLastEddedVisit int;
 Set @IdOfLastEddedVisit =scope_identity();

 INSERT INTO [TheStudyProcess] ([TheStudyProcess].[VisitId],[TheStudyProcess].[StudyId]) 
   VALUES (@IdOfLastEddedVisit,@StudyId);

 declare @IdOfLastEddedStudyProcess int;
 Set @IdOfLastEddedStudyProcess=scope_identity();

 INSERT INTO [Payment] ([Payment].[IsCash],[Payment].[VisitId],[Payment].[StudyProcessId])
   VALUES (@IsCash,@IdOfLastEddedVisit,@IdOfLastEddedStudyProcess);

 INSERT INTO [TheStudyProcessLaborants]([TheStudyProcessLaborants].[TheStudyProcessId]) VALUES (@IdOfLastEddedStudyProcess);
 
 
 COMMIT 
END TRY

 BEGIN CATCH
  ROLLBACK TRAN
 END CATCH
END
