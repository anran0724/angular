using System.Collections.Generic;

namespace Sinodom.ElevatorCloud.Chat.Dto
{
    public class ChatUserWithMessagesDto : ChatUserDto
    {
        public List<ChatMessageDto> Messages { get; set; }
    }
}
