SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetAllChargesByDate
	(
		@DateCharge datetime
	)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT [ListOfCharges].[Id], [ListOfCharges].[ChargeNote], [ListOfCharges].[CostCharge],[ListOfCharges].[DateCharge] 
			FROM [ListOfCharges] Where (([ListOfCharges].[DateCharge]>= convert(datetime,@DateCharge,120))
										and [ListOfCharges].[DateCharge]<dateadd(day,1,convert(datetime,@DateCharge,120)))
    
END
