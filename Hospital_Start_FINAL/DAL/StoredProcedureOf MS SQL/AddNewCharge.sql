USE [Hospital]
GO
/****** Object:  StoredProcedure [dbo].[AddNewCharge]    Script Date: 27.01.2018 17:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Alistratov,,Taras>
-- Create date: <1.05.2017,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AddNewCharge]
	(
		@ChargeNote nvarchar(100),
		@CostCharge numeric(7,2),
		@DateCharge datetime
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

   INSERT INTO [ListOfCharges] ([ListOfCharges].[ChargeNote],[ListOfCharges].[CostCharge],[ListOfCharges].[DateCharge])
				 VALUES (@ChargeNote,@CostCharge,@DateCharge)

END
