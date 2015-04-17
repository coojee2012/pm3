--因证书等级存放了证书类别、名称、等级，长度不够，加长
alter  table   TC_AJBA_Record  alter  column    SGDWZZDJ  varchar(100)
alter  table   TC_AJBA_Record  alter  column    JLDWZZDJ  varchar(100)