using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Worldreaver.Timer;

public class TimerTest : MonoBehaviour
{
    private Timer _timer;

    private void StartTime()
    {
        _timer = new Timer();
        _timer.StartedAsObservable.Subscribe(_ => Debug.Log("Start"));
        _timer.FinishedAsObservable.Subscribe(_ =>
        {
            Debug.Log("Complete!");
            _timer.Stop();
        });
        _timer.StoppedTimeAsObservable.Subscribe(_ => Debug.Log("HAHA it is completed"));
        _timer.PausedTimeAsObservable.Subscribe(_ => Debug.Log("pause"));
        _timer.ResumedAsObservable.Subscribe(_ => Debug.Log("Resume"));
        _timer.Start(10);
        _timer.ElapsedTimeAsObservable.Subscribe(_ => Debug.Log($" Elaspsed :{_}"));
        _timer.RemainTimeAsObservable.Subscribe(_ => Debug.Log($" Remain :{_}"));
        _timer.RemainTimeAsObservable
            .Select(it => Mathf.CeilToInt(it))
            .DistinctUntilChanged()
            .Subscribe(_ => Debug.Log($" Remain :{_}"))
            .AddTo(this);

        _timer.RemainTimeAsObservable
            .Subscribe(time => this.Render(time, _timer.CurrentFinishTime))
            .AddTo(this);
        _timer.IsPlayingAsObservable.Subscribe(_ => Debug.LogWarning("IsPlayingAsObservable : " + _));
    }

    private void Render(float time, float finishTime)
    {
        var ratio = finishTime > 0 ? time / finishTime : 1f;
        Debug.Log($"ratio :{ratio}");
    }


    public void Pause()
    {
        _timer.Pause();
    }

    public void Resume()
    {
        _timer.Resume();
    }

    public void GetCurrentRemainTime()
    {
        Debug.LogWarning($"CurrentRemainTime: {_timer.GetRemainTime()}");
    }

    public void GetCurrentElapsedTime()
    {
        Debug.LogWarning($"CurrentElapsedTime: {_timer.GetElapsedTime()}");
    }

    public void IsPlaying()
    {
        Debug.LogWarning($"IsPlaying: {_timer.IsPlaying}");
    }

    public void Add5()
    {
        _timer.IncreaseTime(5);
    }

    public void Minus5()
    {
        _timer.DecreaseTime(5);
    }
}
