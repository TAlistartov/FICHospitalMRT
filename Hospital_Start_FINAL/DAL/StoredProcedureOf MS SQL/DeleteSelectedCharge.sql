USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[DeleteSelectedCharge]    Script Date: 27.01.2018 18:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Alistratov,,Taras>
-- Create date: <01.05.2017,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[DeleteSelectedCharge]
	(
		@SelectedChargeId int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

   DELETE FROM [ListOfCharges] Where [ListOfCharges].[Id]=@SelectedChargeId

END
