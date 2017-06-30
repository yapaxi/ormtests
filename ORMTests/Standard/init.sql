delete from [dbo].[DecisionReducedMoneyReason];
delete from [dbo].[DecisionRefund];
delete from [dbo].[DecisionLine]
delete from [dbo].[OrderReturnLine]
update [dbo].[OrderReturn]  set CurrentDecisionId = null
delete from [dbo].[Decision]
delete from [dbo].[OrderReturn] 

go
insert into [dbo].[OrderReturn]
(VenueReturnId, SellingVendorId, PurchaseOrderId, VenueCreatedOn, CreatedOn, UpdatedOn, StatusId, StatusMessage, ShippingCarrier, ShippingTrackingNumber, ReturnCharge, RefundWithoutReturn, ReturnWorkflowStepId, IsInRecovery, CurrentDecisionId)
select
  VenueReturnId = newid()
 , SellingVendorId = 1
 , PurchaseOrderId = newid()
 , VenueCreatedOn = getdate()
 , CreatedOn = getdate()
 , UpdatedOn = getdate()
 , StatusId = 1
 , StatusMessage = newid()
 , ShippingCarrier = 1
 , ShippingTrackingNumber = newid()
 , ReturnCharge = 0
 , RefundWithoutReturn = 0
 , ReturnWorkflowStepId = 1
 , IsInRecovery = 0
 , CurrentDecisionId = null

 declare @id int = SCOPE_IDENTITY();


 insert into [dbo].[OrderReturnLine]
 (OrderReturnId, VenueReturnLineId, PurchaseOrderLineId, Quantity, ReasonId, SaleAmount, TaxAmount, ShippingAmount, ShippingTaxAmount, CurrencyCode)
 select top 10 OrderReturnId = @id
 , VenueReturnLineId = NEWID()
 , PurchaseOrderLineId = NEWID()
 , Quantity = 5
 , ReasonId = 1
 , SaleAmount = 100
 , TaxAmount = 100
 , ShippingAmount = 100
 , ShippingTaxAmount = 100
 , CurrencyCode = 'USD'
from sys.columns

 insert into [dbo].[Decision] (Code, CreatedOn, OrderReturnId, PublicDecisionId)
 values (newid(), getdate(), @id, newid())
 
 declare @did int = SCOPE_IDENTITY();

 update [dbo].[OrderReturn] 
 set CurrentDecisionId = @did
 where id = @id

 insert into [dbo].[DecisionLine]
 (OrderReturnLineId, DecisionId)
select id, @did 
from dbo.OrderReturnLine
where OrderReturnId = @id

 insert into [dbo].[DecisionRefund]
 (DecisionLineId, SaleAmount, TaxAmount, ShippingAmount, ShippingTaxAmount, CurrencyCode)
select id, 100, 100, 100, 100, 'USD'
from [dbo].[DecisionLine]
where DecisionId = @did

 insert into [dbo].[DecisionReducedMoneyReason]
 (DecisionLineId, ReducedMoneyReason)
select id, newid()
from [dbo].[DecisionLine]
where DecisionId = @did

go 10000

alter table [dbo].[OrderReturn] rebuild
alter table [dbo].[Decision]rebuild
alter table [dbo].[OrderReturnLine]rebuild
alter table [dbo].[DecisionLine]rebuild
alter table [dbo].[DecisionRefund]rebuild
alter table [dbo].[DecisionReducedMoneyReason]rebuild

create index IX_ASASAS on [OrderReturnLine]([OrderReturnId])