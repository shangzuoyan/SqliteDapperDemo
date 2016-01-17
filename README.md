# SqliteDapperDemo
以Sqlite为Db，Dapper为orm，构成一个轻型的DAL，在查询方面，集成了sqlinq

------------------------------------------------------------------------------------------------------
本demo没有实现dapper的存储过程，在此附上实例代码，以作参考：
//用dapper的事务实现添加订单
        public bool MakeOrder(Order order,List<int> ids)
        {
            int row=0;
            using (IDbConnection conn = GetOpenConnection())
            {
                string addOrderSql = "insert into dbo.Orders(OrderNumber, OrderDate, PayModeId, TotalPrice, UserLoginId, ReceiverName, ReceiverPhone, ReceiverAddress,LeftMessage,DistrictId,OrderStateId,IsFirst) values(@OrderNumber, @OrderDate, @PayModeId, @TotalPrice, @UserLoginId, @ReceiverName, @ReceiverPhone, @ReceiverAddress,@LeftMessage,@DistrictId,@OrderStateId,@IsFirst)";
                string UpdateSateSql = GetUpdateStateSql(ids);

                IDbTransaction transaction = conn.BeginTransaction();
                row = conn.Execute(addOrderSql, order, transaction, null, null);
                row += conn.Execute(UpdateSateSql, new { OrderNumber = order.OrderNumber }, transaction, null, null);
                transaction.Commit();
            }
            if (row > 0)
            {
                return true;
            }
            return false;
        }
  -------------------------------------------------------------------------------------------------------
