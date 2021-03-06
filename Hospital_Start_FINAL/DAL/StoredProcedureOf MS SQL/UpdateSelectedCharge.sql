USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[UpdateSelectedCharge]    Script Date: 01.05.2017 0:28:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Alistratov,,Taras>
-- Create date: <01.05.2017,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[UpdateSelectedCharge] 
	(
		@SelectedChargeId int,
		@ChargeNote nvarchar(100),
		@CostCharge numeric(7,2)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

		Update [ListOfCharges] set [ListOfCharges].[ChargeNote]=@ChargeNote, [ListOfCharges].[CostCharge]=@CostCharge
				Where [ListOfCharges].[Id]=@SelectedChargeId
END
