namespace RosSharp.RosBridgeClient
{
    public class Float32Publisher : Publisher<Messages.Standard.Float32>
    {
        public float messageData;

        private Messages.Standard.Float32 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new Messages.Standard.Float32
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