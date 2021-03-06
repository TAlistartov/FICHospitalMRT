USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[AddNewPatientToInFactMagasine]    Script Date: 30.04.2017 14:28:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AddNewPatientToInFactMagasine] 
	(
		@PatientId int,
		@StudyId int,
		@DateAndTimeOfPreordering datetime,
		@IsPreorder bit,
		@IsNeedSendEmail bit,
		@IsHasVisited bit,
		@IsCash bit,
		@LaborantId1 int, 
		@LaborantId2 int=null, 
		@FinalCost numeric(6,2),
		@NoteForDiscount nvarchar(100), 
		@DoctorData nvarchar(30)
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

 INSERT INTO [TheStudyProcess] ([TheStudyProcess].[VisitId],[TheStudyProcess].[StudyId],[TheStudyProcess].[DoctorData]) 
   VALUES (@IdOfLastEddedVisit,@StudyId,@DoctorData);

 declare @IdOfLastEddedStudyProcess int;
 Set @IdOfLastEddedStudyProcess=scope_identity();

 INSERT INTO [Payment] ([Payment].[IsCash],[Payment].[VisitId],[Payment].[StudyProcessId],[Payment].[NoteForDiscount],
							[Payment].[FinalCost])
   VALUES (@IsCash,@IdOfLastEddedVisit,@IdOfLastEddedStudyProcess,@NoteForDiscount,@FinalCost);

 INSERT INTO [TheStudyProcessLaborants]([TheStudyProcessLaborants].[TheStudyProcessId],[TheStudyProcessLaborants].[LaborantId])
	VALUES (@IdOfLastEddedStudyProcess,@LaborantId1);

	IF(@LaborantId2 IS NOT NULL) 
		BEGIN
			INSERT INTO [TheStudyProcessLaborants]([TheStudyProcessLaborants].[TheStudyProcessId],[TheStudyProcessLaborants].[LaborantId])
						VALUES (@IdOfLastEddedStudyProcess,@LaborantId2)
		END 
 
 COMMIT 
END TRY

 BEGIN CATCH
  ROLLBACK TRAN
 END CATCH
END
