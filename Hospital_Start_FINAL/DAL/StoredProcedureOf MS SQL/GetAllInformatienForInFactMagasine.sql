USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[GetAllInformatienForInFactMagasine]    Script Date: 27.03.2017 22:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetAllInformatienForInFactMagasine]
	(
		@DateForInFactMagasine datetime
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [Patient].[Id],[Patient].[LastName],[Patient].[FirstName],[Patient].[MiddleName],[Patient].[CellPhone],[Patient].[Note],
	[Patient].[BirthDate], [Patient].[Adress], [Patient].[JobPlace], [Patient].[Email],[Study].[Id],[Study].[Type],
	[Visit].[Date],[Visit].[IsNeedSendEmail],[Payment].[IsCash],[Payment].[NoteForDiscount],[Payment].[FinalCost],
	[TheStudyProcess].[DoctorData],[Laborant].[Id], concat([Laborant].[LastName],' ',[Laborant].[FirstName]) As LaborantData,
	[Visit].[IsHasVisited],[Visit].[IsPreorder],[Study].[Cost]
	FROM [Payment] 
	Inner Join [Visit] On [Payment].[VisitId]=[Visit].[Id] 
	Inner Join [Patient] On	[Visit].[PatientId]=[Patient].[Id] 
	Inner Join [TheStudyProcess] On [Payment].[StudyProcessId]=[TheStudyProcess].[Id]
	Inner Join [Study] On [TheStudyProcess].[StudyId]=[Study].[Id] 
	Right Join [TheStudyProcessLaborants] On [TheStudyProcessLaborants].[TheStudyProcessId]=[TheStudyProcess].[Id]
	Left Join [Laborant] On [TheStudyProcessLaborants].[LaborantId]=[Laborant].[Id]
    
	
	Where (([Visit].[Date]>= convert(datetime,@DateForInFactMagasine,120) 
	and [Visit].[Date]<dateadd(day,1,convert(datetime,@DateForInFactMagasine,120)))and [Visit].[IsHasVisited]=(1))
	--Group by [Patient].[Id]
  
END
