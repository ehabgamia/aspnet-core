using System.Collections.Generic;
using MCV.Portal.Auditing.Dto;
using MCV.Portal.Dto;

namespace MCV.Portal.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
