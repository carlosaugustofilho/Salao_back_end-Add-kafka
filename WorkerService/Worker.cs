using Confluent.Kafka;
using salao_app.Models.Requests;
using System.Text.Json;

namespace salao_app.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _agendamentoTopicName;
        private readonly string _kafkaBootstrapServers;
        private readonly string _consumerGroupName;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _agendamentoTopicName = _configuration["KafkaSettings:AgendamentoTopicName"];
            _kafkaBootstrapServers = _configuration["KafkaSettings:BootstrapServers"];
            _consumerGroupName = _configuration["KafkaSettings:ConsumerGroupName"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var config = new ConsumerConfig
                {
                    BootstrapServers = _kafkaBootstrapServers,
                    GroupId = _consumerGroupName,
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(_agendamentoTopicName);

                    CancellationTokenSource cts = new CancellationTokenSource();
                    Console.CancelKeyPress += (_, e) =>
                    {
                        e.Cancel = true;
                        cts.Cancel();
                    };

                    try
                    {
                        while (true)
                        {
                            try
                            {
                                var consumeResult = consumer.Consume(cts.Token);
                                var agendamentoRequest = JsonSerializer.Deserialize<AgendamentoRequest>(consumeResult.Message.Value);

                                // Chame o serviço para processar o agendamento
                                // _service.ProcessAgendamento(agendamentoRequest);

                                Console.WriteLine($"Mensagem recebida: {consumeResult.Message.Value}");
                            }
                            catch (ConsumeException e)
                            {
                                Console.WriteLine($"Erro ao consumir a mensagem: {e.Error.Reason}");
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                    }
                }
            }
        }
    }
}
