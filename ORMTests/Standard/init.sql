delete from [dbo].[DecisionReducedMoneyReason] with (tablockx);
delete from [dbo].[DecisionRefund] with (tablockx);
delete from [dbo].[DecisionLine] with (tablockx)
delete from [dbo].[OrderReturnLine] with (tablockx)
update [dbo].[OrderReturn] with (tablockx)  set CurrentDecisionId = null
delete from [dbo].[Decision] with (tablockx)
delete from [dbo].[OrderReturn]  with (tablockx)
delete from dbo.OrderReturnReason with (tablockx)

go

insert into dbo.OrderReturnReason (Id, MarketplaceId, MarketplaceReason)values (1,1,'aa')

insert into [dbo].[OrderReturn] with (tablockx)
(VenueReturnId, SellingVendorId, PurchaseOrderId, VenueCreatedOn, CreatedOn, UpdatedOn, StatusId, StatusMessage, ShippingCarrier, ShippingTrackingNumber, ReturnCharge, RefundWithoutReturn, ReturnWorkflowStepId, IsInRecovery, CurrentDecisionId)
select top 100000
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
from sys.columns c1
cross join sys.columns c2
cross join sys.columns c3

 insert into [dbo].[OrderReturnLine] with (tablockx)
 (OrderReturnId, VenueReturnLineId, PurchaseOrderLineId, Quantity, ReasonId, SaleAmount, TaxAmount, ShippingAmount, ShippingTaxAmount, CurrencyCode)
 select OrderReturnId = r.Id
 , VenueReturnLineId = NEWID()
 , PurchaseOrderLineId = NEWID()
 , Quantity = 5
 , ReasonId = 1
 , SaleAmount = 100
 , TaxAmount = 100
 , ShippingAmount = 100
 , ShippingTaxAmount = 100
 , CurrencyCode = 'USD'
from [dbo].[OrderReturn] r
cross join (select top 10 1 as t from sys.columns x) as t

 insert into [dbo].[Decision]  with (tablockx)
 (Code, CreatedOn, OrderReturnId, PublicDecisionId)
 select newid(), getdate(), r.Id, newid() 
 from dbo.OrderReturn r

 update x with (tablockx)
 set x.CurrentDecisionId = d.Id
 from [dbo].[OrderReturn] x
 join dbo.Decision d on d.OrderReturnId = x.id

 insert into [dbo].[DecisionLine] with (tablockx)
 (OrderReturnLineId, DecisionId)
select l.id, r.CurrentDecisionId
from dbo.OrderReturnLine l
join dbo.OrderReturn r on l.OrderReturnId = r.Id

 insert into [dbo].[DecisionRefund] with (tablockx)
 (DecisionLineId, SaleAmount, TaxAmount, ShippingAmount, ShippingTaxAmount, CurrencyCode)
select id, 100, 100, 100, 100, 'USD'
from [dbo].[DecisionLine]

 insert into [dbo].[DecisionReducedMoneyReason] with (tablockx)
 (DecisionLineId, ReducedMoneyReason)
select id, newid()
from [dbo].[DecisionLine]

go

alter table [dbo].[OrderReturn] rebuild
alter table [dbo].[Decision]rebuild
alter table [dbo].[OrderReturnLine]rebuild
alter table [dbo].[DecisionLine]rebuild
alter table [dbo].[DecisionRefund]rebuild
alter table [dbo].[DecisionReducedMoneyReason]rebuild

create index IX_ASASAS on [OrderReturnLine]([OrderReturnId])
