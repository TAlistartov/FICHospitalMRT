-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AddPatientToList
	(
		@patientLastName nvarchar(30),
		@patientFirstName nvarchar(30),
		@patientMiddleName nvarchar(30),
		@patientCellPhone nvarchar(30),
		@patientBirthDate datetime,
		@patientAdress nvarchar(150),
		@patientJobPlace nvarchar(50),
		@patientEmail nvarchar(50),
		@patientNote nvarchar(500)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO [Patient] ([Patient].[LastName], [Patient].[FirstName], [Patient].[MiddleName],[Patient].[CellPhone],
	[Patient].[BirthDate],[Patient].[Adress],[Patient].[JobPlace],[Patient].[Email],[Patient].[Note]) 
					VALUES (@patientLastName,@patientFirstName,@patientMiddleName,@patientCellPhone,@patientBirthDate,@patientAdress,
							@patientJobPlace,@patientEmail,@patientNote)
END
GO
