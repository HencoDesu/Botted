using System;
using Botted.Core.Events;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Events.Abstractions.Extensions;
using FluentAssertions;
using Xunit;

namespace Botted.Tests.CoreTests
{
	public class EventServiceTest
	{
		class TestEvent : EventWithData<int> { }

		[Fact]
		public void SubscribeUnsubscribeTest()
		{
			var eventService = new EventService();
			var invocations = 0;
			
			var subscription = eventService.GetEvent<TestEvent>()
										   .Subscribe(() => invocations++);
			eventService.Raise<TestEvent>();
			invocations.Should().Be(1);
			
			subscription.Dispose();
			eventService.Raise<TestEvent>();
			invocations.Should().Be(1);
		}

		[Fact]
		public void SyncSubscribeUnsubscribeWithDataTest()
		{
			var eventService = new EventService();
			var invocations = 0;
			
			var subscription = eventService.GetEvent<TestEvent>()
										   .Subscribe(_ => invocations++);
			eventService.Raise<TestEvent, int>(invocations);
			invocations.Should().Be(1);
			
			subscription.Dispose();
			eventService.Raise<TestEvent, int>(invocations);
			invocations.Should().Be(1);
		}
	}
}