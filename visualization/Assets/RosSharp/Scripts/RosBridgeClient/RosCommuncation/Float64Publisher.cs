namespace RosSharp.RosBridgeClient
{
    public class Float64Publisher : Publisher<Messages.Standard.Float64>
    {
        public float messageData;

        private Messages.Standard.Float64 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new Messages.Standard.Float64
            {
                data = messageData
            };
        }

        private void Update()
        {
            message.data = messageData;
            Publish(message);
        }
    }
}