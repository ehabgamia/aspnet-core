using System.Collections.Generic;
using MCV.Portal.Chat.Dto;
using MCV.Portal.Dto;

namespace MCV.Portal.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(List<ChatMessageExportDto> messages);
    }
}
