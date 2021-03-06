﻿using HostManager.Contracts;
using Microsoft.Extensions.Hosting;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostManager.Services
{
    public class CronJobService : BackgroundService
    {
        private readonly ICheckExpirationService _check;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private string Schedule => "00 12 * * * ";

        public CronJobService(ICheckExpirationService check)
        {
            _check = check;

            _schedule = CrontabSchedule.Parse(Schedule);

            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {
                    await _check.CheckExpiration();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}
