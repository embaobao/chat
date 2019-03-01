--CREATE DATABASE wechart--创建数据库，名称wechart
--ON
--(
--    NAME = wechart, --创建数据库，数据存名称 wechart
--    FILENAME = 'd:\wechart.mdf',--创建数据库路径 d 盘
--    SIZE = 5MB,--创建数据库 大小为5mb
--    MAXSIZE = 20MB,--创建数据库 最大20mb
--    FILEGROWTH = 20--创建数据库 文件可变增长 20mb
--)
--LOG ON 
--(
--    NAME = wechartlog,--创建数据库日志文件
--    FILENAME = 'd:\wechart.ldf',--创建数据库日志文件路径 
--    SIZE = 2MB,--创建数据库日志大小
--    MAXSIZE = 10MB,--创建数据库 最大
--    FILEGROWTH = 1MB--创建数据库 增量
--);


--use wechart ;

--create table userstb --记录用户账户密码
--(
--uname varchar(max) ,--记录账户名
--upw  varchar(max)--记录账户密码
--)
--create table userinfo--记录用户信息
--(
--uname varchar(max),--记录账户名
--uip  varchar(max),--记录用户登录ip
--upip  varchar(max),--记录用户上次ip
--uregistertime datetime ,--记录用户注册时间
--uptime datetime,--记录用户上次登录时间
--usex varchar(max),--记录用户性别
--uage int,--记录账户年龄
--ulocimgpath varchar(max),--记录用户电脑头像路径
--uimg varchar(max),--记录头像服务器路径
--utxt varchar(max)--记录用户签名
--)
--create table userfm--记录用户好友
--(
--uname varchar(max),--记录名
--uflist varchar(max),--记录用户好友列表
--umlist varchar(max),--记录用户应接受消息列表
--)
--create table allmess --记录所有服务器转发消息
--(
--messid int identity(1,1),--记录所有消息id
--fname varchar(max),--记录发送人
--toname varchar(max),--记录接收人
--txt varchar(max),--记录内容
--messtime datetime--记录消息发送时间
--)
--create table usermesstb--记录用户消息
--(
--uname varchar(max),--记录该信息所属人
--fname varchar(max),--记录信息发送者
--toname varchar(max),--记录信息接受者
--txt varchar(max),--记录内容
--messtime datetime--记录信息时间
--)

--删除表
--use wechart
--drop table userfm;
--drop table allmess;
--drop table userstb;
--drop table userinfo;
--drop table usermesstb;
--drop database wechart; --删除数据库；、





--创建注册触发器，当用户注册时，
--向userfm 注册用户名 好友列表项，好友消息项，
--向userinfo 注册用户名 注册时间，性别初始化男，年龄0,ip null,upip null,uptime,签名
--use wechart

--use wechart 
--go 
--if OBJECT_ID('registerid','TR') is not null
--drop trigger  registerid
--go
--create trigger  registerid  
--on userstb after insert
--as
--begin
--declare @name varchar(max)
--select @name =inserted.uname from inserted
--insert into userfm(uname,uflist)values(@name,'server;')
--insert into userinfo values(@name,'','',GETDATE(),GETDATE(),'先生',0,'','','我就是我，不一样的烟火')
--end
--go



--insert into userstb(uname)values('123')


--创建消息记录触发器，当用户发送消息时，
--use wechart

--use wechart 
--go 
--if OBJECT_ID('addusermess','TR') is not null
--drop trigger  addusermess
--go
--create trigger addusermess
--on allmess after insert
--as
--begin

--if  exists(select * from userstb,inserted where uname=inserted.fname or uname=inserted.toname)
-- begin 
--declare @fromname varchar(max) , @tname varchar(max) ,@contxt varchar(max)
--select @fromname=inserted.fname from inserted
--select @tname=inserted.toname from inserted
--select @contxt=inserted.txt from inserted
--insert into usermesstb values(@fromname,@fromname,@tname,@contxt,GETDATE())
--insert into usermesstb values(@tname,@fromname,@tname,@contxt,GETDATE())
-- end
-- end
--insert into allmess(fname,toname,txt) values('123','123',456)
--insert into allmess(fname,toname,txt) values('13','423',456)



--select * from allmess;
select ulocimgpath from userinfo;