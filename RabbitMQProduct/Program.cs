//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SmsAPI;

var _list = new List<string>();
//Consumer: An application responsible for messages.
//  مصرف کننده: برنامه ای که مسئول پیام هاست.
#region Get_RabbitMQ_message_By_List_Json_1
var factory = new ConnectionFactory
{
    HostName = "localhost"
};
//Create the RabbitMQ connection using connection factory details as i mentioned above
var connection = factory.CreateConnection();
//Here we create channel with session and model
using
var channel = connection.CreateModel();
//declare the queue after mentioning name and a few property related to that
channel.QueueDeclare("product", exclusive: false);
//Set Event object which listen message from chanel which is sent by producer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    _list.Add(message);

    Console.WriteLine($"Product message received From RabbitMQ: {message}");

    var top = new ServiceLogInfo();
    top.InsertLogInfo();

};

//read the message
channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);

#endregion
#region  Get_RabbitMQ_message_By_Variable_Json_2
//IS Correct Insert Flag By RabbitMQ 
var factory2 = new ConnectionFactory
{
    HostName = "localhost"
};
//Create the RabbitMQ connection using connection factory details as i mentioned above
var connection2 = factory2.CreateConnection();
//Here we create channel with session and model
using
var channel2 = connection2.CreateModel();
//declare the queue after mentioning name and a few property related to that
channel2.QueueDeclare("productIsCorrect", exclusive: false);
//Set Event object which listen message from chanel which is sent by producer
var consumer2 = new EventingBasicConsumer(channel2);
consumer2.Received += (model2, eventArgs2) =>
{
    var body2 = eventArgs2.Body.ToArray();
    var message2 = System.Text.Encoding.UTF8.GetString(body2);
    var test2=JsonConvert.DeserializeObject(message2.ToString());

    Console.WriteLine($"ProductIsCorrect message received From RabbitMQ: {message2}");
    Console.WriteLine($"ProductIsCorrect message received From RabbitMQ: {test2}");
    var top = new ServiceLogInfo();
    top.InsertLogInfo();

};
//read the message
channel.BasicConsume(queue: "productIsCorrect", autoAck: true, consumer: consumer2);


#endregion
Console.ReadKey();
