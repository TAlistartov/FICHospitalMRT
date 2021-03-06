USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[GetAllInformatienOfPatientForPreorderMagasine]    Script Date: 17.04.2017 19:51:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: GetAllInformatienOfPatientForPreorderMagasine.sql|7|0|C:\Users\Pavlog_3\Documents\SQL Server Management Studio\GetAllInformatienOfPatientForPreorderMagasine.sql
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetAllInformatienOfPatientForPreorderMagasine] 
	(
		@DateOnPreordering datetime
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
	SELECT [Visit].[Date],[Patient].[LastName],[Patient].[FirstName],[Patient].[MiddleName],[Patient].[CellPhone],[Study].[Type],
	[Patient].[Note], [Patient].[Id],[Patient].[BirthDate], [Patient].[Adress], [Patient].[JobPlace], [Patient].[Email],[Study].[Id],[Study].[Cost] 
	FROM [Payment] Inner Join [Visit] On [Payment].[VisitId]=[Visit].[Id] Inner Join [Patient] On
	[Visit].[PatientId]=[Patient].[Id] Inner Join [TheStudyProcess] On [Payment].[StudyProcessId]=[TheStudyProcess].[Id]
	Inner Join [Study] On [TheStudyProcess].[StudyId]=[Study].[Id] WHERE (([Visit].[Date]>= convert(datetime,@DateOnPreordering,120) 
	and [Visit].[Date]<dateadd(day,1,convert(datetime,@DateOnPreordering,120)))and [Visit].[IsPreorder]=(1))
				 													
END
