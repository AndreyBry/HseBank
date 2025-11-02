using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Services;
using System.Diagnostics;

namespace HseBank.src.Infrastructure.Commands
{
    public class TimedCommandDecorator : ICommand
    {
        private ICommand _decoratedCommand;
        private IMetricsService _metricsService;
        
        public TimedCommandDecorator(ICommand decoratedCommand, IMetricsService metricsService)
        {
            _decoratedCommand = decoratedCommand;
            _metricsService = metricsService;
        }

        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _decoratedCommand.Execute();
                stopwatch.Stop();
                ReportMetrics(stopwatch.Elapsed, true);
            }
            catch (Exception)
            {
                stopwatch.Stop();
                ReportMetrics(stopwatch.Elapsed, false);
                throw;
            }
        }

        public void Undo()
        {
            _decoratedCommand.Undo();
        }

        private string CommandName => _decoratedCommand.GetType().ToString().Split('.')[^1];

        private void ReportMetrics(TimeSpan elapsed, bool successful)
        {
            _metricsService.RecordCommandExecution(CommandName, elapsed, successful);
        }
    }
}
