using KafkaConsumer.Services;

var store = new ItemStore();
var consumerService = new KafkaConsumerService(store);
consumerService.Start();