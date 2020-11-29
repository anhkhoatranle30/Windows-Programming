go
create database WP_Project2_WeSplit
go
use WP_Project2_WeSplit
go

create table MEMBER
(
	MemberID int identity(1,1) primary key,
	MemberName nvarchar(50)
)

create table TRIP
(
	TripID int identity(1, 1) primary key,
	TripName nvarchar(50),
	Status int
)

create table TRIPIMAGES
(
	TripID int,
	Path varchar(50),
	primary key (TripID, Path),
	foreign key (TripID) references TRIP(TripID)
)

create table LOCATION 
(
	TripID int,
	LocationName nvarchar(50),
	primary key (TripID, LocationName),
	foreign key (TripID) references TRIP(TripID)
)

create table MEMBERSPERTRIP 
(
	TripID int,
	MemberID int,
	primary key (TripID, MemberID),
	foreign key(TripID) references TRIP(TripID),
	foreign key (MemberID) references MEMBER(MemberID)
)

create table MEMBERCOST 
(
	MemberID int,
	TripID int,
	CostName nvarchar(50),
	Cost int,
	primary key (MemberID, TripID, CostName),
	foreign key (TripID, MemberID) references MEMBERSPERTRIP(TripID, MemberID)
)