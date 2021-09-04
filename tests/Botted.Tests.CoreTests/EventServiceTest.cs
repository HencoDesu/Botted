using System;
using System.Threading;
using System.Threading.Tasks;
using Botted.Core.Events;
using Botted.Core.Events.Abstractions.Events;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Sdk;

namespace Botted.Tests.CoreTests
{
	public class EventServiceTest
	{
		class TestEvent : EventWithData<int> { }

		private readonly ILogger<EventService> _logger = A.Fake<ILogger<EventService>>();

		[Fact]
		public async Task SyncSubscribeUnsubscribeTest()
		{
			var eventService = new EventService(_logger);
			int invocations = 0;
			
			var subscription = eventService.Subscribe<TestEvent>(TestHandler);
			await eventService.RaiseAndWait<TestEvent>();
			invocations.Should().Be(1);
			
			subscription.Dispose();
			await eventService.RaiseAndWait<TestEvent>();
			invocations.Should().Be(1);

			void TestHandler()
			{
				invocations++;
			}
		}
		
		[Fact]
		public async Task AsyncSubscribeUnsubscribeTest()
		{
			var eventService = new EventService(_logger);
			int invocations = 0;
			
			var subscription = eventService.Subscribe<TestEvent>(TestHandler);
			await eventService.RaiseAndWait<TestEvent>();
			invocations.Should().Be(1);
			
			subscription.Dispose();
			await eventService.RaiseAndWait<TestEvent>();
			invocations.Should().Be(1);

			Task TestHandler()
			{
				invocations++;
				return Task.CompletedTask;
			}
		}
		
		[Fact]
		public async Task SyncSubscribeUnsubscribeWithDataTest()
		{
			var eventService = new EventService(_logger);
			int invocations = 0;
			
			var subscription = eventService.Subscribe<TestEvent, int>(TestHandler);
			await eventService.RaiseAndWait<TestEvent, int>(invocations);
			invocations.Should().Be(1);
			
			subscription.Dispose();
			await eventService.RaiseAndWait<TestEvent, int>(invocations);
			invocations.Should().Be(1);

			void TestHandler(int data)
			{
				invocations++;
			}
		}
		
		[Fact]
		public async Task AsyncSubscribeUnsubscribeWithDataTest()
		{
			var eventService = new EventService(_logger);
			int invocations = 0;
			
			var subscription = eventService.Subscribe<TestEvent, int>(TestHandler);
			await eventService.RaiseAndWait<TestEvent, int>(invocations);
			invocations.Should().Be(1);
			
			subscription.Dispose();
			await eventService.RaiseAndWait<TestEvent, int>(invocations);
			invocations.Should().Be(1);

			Task TestHandler(int data)
			{
				invocations++;
				return Task.CompletedTask;
			}
		}

		[Fact]
		public async Task QueuedEventTest()
		{
			var eventService = new EventService(_logger);
			await eventService.RaiseAndWait<BotStarted>();
			
			eventService.Subscribe<TestEvent>(TestHandler);
			eventService.Raise<TestEvent>();

			eventService.Subscribe<TestEvent>(TestExceptionHandler);
			eventService.Raise<TestEvent, int>(1);

			void TestHandler()
			{ }

			void TestExceptionHandler()
			{
				throw new Exception();
			}
		}
	}
}