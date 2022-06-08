select * from services SR
inner join ServiceDetails SD on SD.ServiceId=SR.ServiceId
inner join Equipments EQ on EQ.EquipmentId=SD.EquipmentId
inner join MaterUsed MTU on MTU.ServiceDetailId=SD.ServiceDetailId
inner join Materials M on M.MaterialId=MTU.MaterialId

inner join RequiredMaterials RQM on RQM.ServiceDetailId =SD.ServiceDetailId
inner join Materials MT on MT.MaterialId=RQM.MaterialId

inner join ServicePictures SP on SP.ServiceDetailId=SD.ServiceDetailId
inner join PictureTypes PTY on PTY.PictureTypeId=SP.PictureTypeId




select CONCAT_WS(' ',U.FirstName,U.Lastname) as CreaterName ,CONCAT_WS(' ',UT.FirstName,UT.Lastname) as TechnicianName,
BR.BranchName,CO.Name CompanyName,ST.ServiceTypeName ,SR.* from services SR
inner join  Branchs BR on BR.branchId=SR.BranchId
inner join Companies CO on CO.CompanyID=BR.compnayId
inner join Users U on U.UserId=SR.CreatedBy
inner join Users UT on UT.UserId=SR.TechnicianId
inner join ServiceType ST on ST.ServiceTypeId=SR.ServiceTypeId


select * from ServiceDetails SD where SD.ServiceId=1

select * from  Equipments EQ where EQ.EquipmentId=1

select * from MaterUsed MTU 
inner join Materials M on M.MaterialId=MTU.MaterialId
where MTU.ServiceDetailId=1

select * from RequiredMaterials RQM
inner join Materials MT on MT.MaterialId=RQM.MaterialId
where RQM.ServiceDetailId=1

select * from ServicePictures SP
inner join PictureTypes PTY on PTY.PictureTypeId=SP.PictureTypeId
where SP.ServiceDetailId=1