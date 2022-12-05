USE sneakerShop
GO

DROP TRIGGER tg_DonHangDuocGiaoSoLuongBanTang

CREATE TRIGGER [dbo].[tg_DonHangDuocGiaoSoLuongBanTang] on [dbo].[Order]
/*
Trigger hoạt động khi có status của Order update thành 0
Trigger thực hiện:
	+ Cộng số lượng bán được tương ứng trong bảng Product (amount)
	+ Trừ số lượng tồn khô tương ứng trong bảng Stock (inStock)
*/
AFTER UPDATE, INSERT
AS
	declare @newStatus tinyint, @cartID int
			
BEGIN
	select @newStatus=ne.[status], @cartID=ne.cartID
	from inserted ne 
	if (@newStatus = 0)
	Begin
		update Product set amount = amount + quantity 
		from Product join CartItem 
		on Product.productId = CartItem.productId 
		where CartItem.cartId = @cartID		

		update Stock set inStock = inStock - quantity 
		from Stock join CartItem 
		on Stock.productId = CartItem.productId 
		where CartItem.cartId = @cartID AND Stock.[size] = CartItem.[size]
	End
END