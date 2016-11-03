﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Innovator.Client
{
  internal static class HttpClientExtensions
  {
    public static IPromise<IHttpResponse> PostPromise(this HttpClient service, Uri uri, bool async, HttpRequest req)
    {
      var hReq = req as IHttpRequest;
      var timeout = new TimeoutSource();
      timeout.CancelAfter(hReq == null ? HttpRequest.DefaultTimeout : hReq.Timeout);

      req.RequestUri = uri;
      req.Method = HttpMethod.Post;

      var result = service.SendAsync(req, timeout.Source.Token)
        .ContinueWith((Func<Task<HttpResponseMessage>, Task<IHttpResponse>>)HttpResponse.Create, TaskScheduler.Default)
        .Unwrap()
        .ToPromise(timeout.Source);
      if (!async)
        result.Wait();
      return result;
    }
    public static IPromise<IHttpResponse> GetPromise(this HttpClient service, Uri uri, bool async, HttpRequest req = null)
    {
      var timeout = new TimeoutSource();
      timeout.CancelAfter(req == null ? HttpRequest.DefaultTimeout : req.Timeout);

      Task<HttpResponseMessage> respTask;
      if (req == null)
      {
        respTask = service.GetAsync(uri, timeout.Source.Token);
      }
      else
      {
        req.RequestUri = uri;
        req.Method = HttpMethod.Get;
        respTask = service.SendAsync(req, timeout.Source.Token);
      }

      var result = respTask
        .ContinueWith((Func<Task<HttpResponseMessage>, Task<IHttpResponse>>)HttpResponse.Create, TaskScheduler.Default)
        .Unwrap()
        .ToPromise(timeout.Source);
      if (!async)
        result.Wait();
      return result;
    }

    private class TimeoutSource
    {
      private CancellationTokenSource _source = new CancellationTokenSource();
      private Timer _timer;

      public CancellationTokenSource Source { get { return _source; } }

      public void CancelAfter(int millisecondsDelay)
      {
        if (_source.IsCancellationRequested)
          return;

        if (_timer == null)
        {
          var timer = new Timer(_timerCallback, this, -1, -1);
          if (Interlocked.CompareExchange<Timer>(ref _timer, timer, null) != null)
          {
            timer.Dispose();
          }
        }
        try
        {
          this._timer.Change(millisecondsDelay, -1);
        }
        catch (ObjectDisposedException) { }
      }

      private static readonly TimerCallback _timerCallback = new TimerCallback(TimerCallbackLogic);

      private static void TimerCallbackLogic(object obj)
      {
        var source = (TimeoutSource)obj;
        try
        {
          source._source.Cancel();
        }
        catch (ObjectDisposedException) { }
      }
    }
  }
}