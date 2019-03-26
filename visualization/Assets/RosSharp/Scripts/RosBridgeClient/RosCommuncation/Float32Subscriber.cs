namespace RosSharp.RosBridgeClient
{
    public class Float32Subscriber : Subscriber<Messages.Standard.Float32>
    {
        public float messageData;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(Messages.Standard.Float32 message)
        {
            messageData = message.data;
        }
    }
}