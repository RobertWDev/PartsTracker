﻿using Microsoft.Extensions.Options;
using Quartz;

namespace PartsTracker.Modules.Parts.Infrastructure.Inbox;

internal sealed class ConfigureProcessInboxJob(IOptions<InboxOptions> outboxOptions)
    : IConfigureOptions<QuartzOptions>
{
    private readonly InboxOptions _inboxOptions = outboxOptions.Value;

    public void Configure(QuartzOptions options)
    {
        string jobName = typeof(ProcessInboxJob).FullName!;

        options
            .AddJob<ProcessInboxJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_inboxOptions.IntervalInSeconds).RepeatForever()));
    }
}
