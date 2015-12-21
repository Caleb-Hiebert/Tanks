using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class ChatCore : Bolt.GlobalEventListener {

    public delegate void OnChatMessage(string formattedMessage);
    public static event OnChatMessage ChatMessageReceived;

    public struct ChatMessage
    {
        string message;
        BoltEntity sender;
        string formatted;
        MessageTypes messageType;

        public ChatMessage(string message, BoltEntity sender, MessageTypes messageType)
        {
            this.message = message;
            this.sender = sender;
            this.formatted = FormatMessage(message, sender, messageType);
            this.messageType = messageType;
        }

        public ChatMessage(BoltEntity killer, BoltEntity victim)
        {
            message = FormatDeathMessage(killer, victim);
            sender = null;
            formatted = message;
            messageType = MessageTypes.DeathMessage;
        }

        public string Message
        {
            get { return message; }
        }

        public BoltEntity Sender
        {
            get { return sender; }
        }

        public string Formatted
        {
            get { return formatted; }
        }

        public MessageTypes MessageType
        {
            get { return messageType; }
        }
    }

    public enum MessageTypes
    {
        PlayerMessage, ServerMessage, DeathMessage
    }

    static List<ChatMessage> messages = new List<ChatMessage>();

    public override void SceneLoadLocalBegin(string map)
    {
        messages.Clear();
    }

    public override void OnEvent(ChatEvent evnt)
    {
        ChatMessage m;

        if(evnt.Sender == null)
        {
            m = new ChatMessage(evnt.Message, evnt.Sender, MessageTypes.ServerMessage);
        } else
        {
            m = new ChatMessage(evnt.Message, evnt.Sender, MessageTypes.PlayerMessage);
        }

        messages.Add(m);

        if (ChatMessageReceived != null)
        {
            ChatMessageReceived(m.Formatted);
        }
    }

    public override void OnEvent(DeathEvent evnt)
    {
        var m = new ChatMessage(evnt.Killer, evnt.Victim);

        messages.Add(m);

        if(ChatMessageReceived != null)
        {
            ChatMessageReceived(m.Formatted);
        }
    }

    public static string FormatMessage(string message, BoltEntity sender, MessageTypes type)
    {
        if(type == MessageTypes.ServerMessage)
        {
            return string.Format("<color=#ff00ffff>{0}</color>", message);
        } else if(type == MessageTypes.PlayerMessage)
        {
            return string.Format("{0}: {1}", sender.GetState<IPlayer>().Name, message);
        } else
        {
            return null;
        }
    }

    public static string FormatDeathMessage(BoltEntity killer, BoltEntity victim)
    {
        if(killer != null)
        {
            if (killer.StateIs<IPlayer>())
            {
                return string.Format("<color=#e68a00>{0} rekt {1}</color>", killer.GetState<IPlayer>().Name, victim.GetState<IPlayer>().Name);
            } else
            {
                return string.Format("<color=#e68a00>{0} rekt {1}</color>", killer.gameObject.name, victim.GetState<IPlayer>().Name);
            }
        } else
        {
            return string.Format("<color=#ffffff>{0} commited suicide!</color>", victim.GetState<IPlayer>().Name);
        }
    }

    public static ChatMessage[] ChatMessages
    {
        get { return messages.ToArray(); }
    }
    
    public static string FormattedStringAll
    {
        get
        {
            string r = string.Empty;

            foreach (var item in messages)
            {
                r += "\n" + item.Formatted;
            }

            return r;
        }
    }

    public static void Send(string message, BoltEntity sender)
    {
        if (!BoltNetwork.isRunning)
            return;

        var msg = ChatEvent.Create(Bolt.GlobalTargets.Everyone, Bolt.ReliabilityModes.ReliableOrdered);
        msg.Sender = sender;
        msg.Message = message;
        msg.Send();
    }

    public static void SendServerMessage(string message)
    {
        if (!BoltNetwork.isServer)
            return;

        var msg = ChatEvent.Create(Bolt.GlobalTargets.Everyone, Bolt.ReliabilityModes.ReliableOrdered);
        msg.Sender = null;
        msg.Message = message;
        msg.Send();
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        var t = (ConnectToken)connection.ConnectToken;

        SendServerMessage(t.name + " has joined.");
    }

    public override void Disconnected(BoltConnection connection)
    {
        if(BoltNetwork.isConnected)
            SendServerMessage(((Player)connection.UserData).character.GetState<IPlayer>().Name + " has left.");
    }
}
