using Android.App;
using Android.Widget;
using Android.OS;
using static Android.Views.View;
using Android.Views;
using System;
using Android.Util;
using Android.Graphics;

namespace DragableView_Demo
{
    [Activity(Label = "DragableView_Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnTouchListener
    {
        Button dragAbleBt;
        ImageView imgV1;
        int screenWidth = 0;
        int screenHeight = 0;
        int lastX = 0, lastY = 0;
        public bool OnTouch(View v, MotionEvent e)
        {
              
            MotionEventActions ea = e.Action;
            switch (ea) {
                case MotionEventActions.Down:
                    lastX = (int)e.RawX;
                    lastY = (int)e.RawY;                
                    break;
                case MotionEventActions.Move:
                    int dx = (int)e.RawX - lastX;
                    int dy = (int)e.RawY - lastY;
                    int left = v.Left + dx;
                    int right = v.Right + dx;
                    int top = v.Top + dy;
                    int bottom = v.Bottom + dy;
                    if (left < 0)
                    {
                        left = 0;
                        right = left + v.Width;
                    }
                    if (right > screenWidth)
                    {
                        right = screenWidth;
                        left = right - v.Width;
                    }
                    if (top < 0)
                    {
                        top = 0;
                        bottom = top + v.Height;
                    }
                    if (bottom > screenHeight)
                    {
                        bottom = screenHeight;
                        top = bottom - v.Height;
                    }
                    v.Layout(left, top, right, bottom);
                    lastX = (int) e.RawX;
                    lastY = (int) e.RawY;
                    v.PostInvalidate();
                    break;
                case MotionEventActions.Up:                  
                    break;                   
            }
            if (v.Id == Resource.Id.imageView1)
            {
                return true;
            }
            return false;
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            //DisplayMetrics dm = Resources.DisplayMetrics;
            //screenWidth = dm.WidthPixels;
            //screenHeight = dm.HeightPixels;
            dragAbleBt = FindViewById<Button>(Resource.Id.button1);
            imgV1 = FindViewById<ImageView>(Resource.Id.imageView1);
            dragAbleBt.SetOnTouchListener(this);
            imgV1.SetOnTouchListener(this);
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            if (hasFocus)
            {
                Rect outRect = new Rect();
                this.Window.FindViewById(Window.IdAndroidContent).GetDrawingRect(outRect);
                screenWidth = outRect.Width();
                screenHeight = outRect.Height();
            }
        }
    }
}

