using System;
using UnityEngine;

namespace IceFrostEases
{
    public static class IceFrostEase
    {
        private const float f0_3 = 0.3f;
        private const float f0_25 = 0.25f;
        private const float f7_5625 = 7.5625f;
        private const float f2_75 = 2.75f;
        private const float f1_5 = 1.5f;
        private const float f0_75 = 0.75f;
        private const float f2_25 = 2.25f;
        private const float f0_9375 = 0.9375f;
        private const float f0_984375 = 0.984375f;
        private const float f2_625 = 2.625f;
        private const float f2_5 = 2.5f;
        private const float f1_70158 = 1.70158f;
        private const float f1_525 = 1.525f;
        private const float f0_5 = 0.5f;

        private static class Back
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d;

                return c * t * t * ((f1_70158 + 1) * t - f1_70158) + b;
            }

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t = t / d - 1;

                return c * (t * t * ((f1_70158 + 1) * t + f1_70158) + 1) + b;
            }

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                float s = f1_70158;

                t /= d / 2;

                s *= f1_525;

                if(t < 1) return c / 2 * (t * t * ((s + 1) * t - s)) + b;

                t -= 2;

                return c / 2 * (t * t * ((s + 1) * t + s) + 2) + b;
            }
        }

        private static class Bounce
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
                => c
                - EaseOut
                (
                    d - t,
                    0,
                    c,
                    d
                )
                + b;

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d;

                if(t < 1 / f2_75) return c * (f7_5625 * t * t) + b;

                if(t < 2 / f2_75)
                {
                    t -= f1_5 / f2_75;

                    return c * (f7_5625 * t * t + f0_75) + b;
                }

                if(t < f2_5 / f2_75)
                {
                    t -= f2_25 / f2_75;

                    return c * (f7_5625 * t * t + f0_9375) + b;
                }

                t -= f2_625 / f2_75;

                return c * (f7_5625 * t * t + f0_984375) + b;
            }

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                if(t < d / 2)
                {
                    return EaseIn
                    (
                        t * 2,
                        0,
                        c,
                        d
                    )
                    * f0_5
                    + b;
                }

                return EaseOut
                (
                    t * 2 - d,
                    0,
                    c,
                    d
                )
                * f0_5
                + c * f0_5
                + b;
            }
        }

        private static class Circ
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d;

                return -c * (MathF.Sqrt(1 - t * t) - 1) + b;
            }

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t = t / d - 1;

                return c * MathF.Sqrt(1 - t * t) + b;
            }

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
            
                t /= d / 2;
                if(t < 1) return-c / 2 * (MathF.Sqrt(1 - t * t) - 1 + b);
            
                t -= 2;
                return c / 2 * ((MathF.Sqrt(1 - t * t) + 1) + b);
            }
        }

        private static class Cubic
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d;

                return c * t * t * t + b;
            }

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t = t / d - 1;

                return c * (t * t * t + 1) + b;
            }

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d / 2;
                if(t < 1) return c / 2 * t * t * t + b;
            
                t -= 2;
                return c / 2 * (t * t * t + 2) + b;
            }
        }

        private static class Elastic
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                if(Mathf.Approximately(t, 0)) return b;

                t /= d;

                if(Mathf.Approximately(t, 1)) return b + c;

                float p = d * f0_3;
                float s = p * f0_25;

                t -= 1;

                return-(c * MathF.Pow(2, 10 * t) * MathF.Sin((t * d - s) * (2 * MathF.PI) / p)) + b;
            }

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                if(Mathf.Approximately(t, 0)) return b;

                t /= d;
            
                if(Mathf.Approximately(t, 1)) return b + c;

                float p = d * f0_3;
                float s = p * f0_25;

                return c * MathF.Pow(2, -10 * t) * MathF.Sin((t * d - s) * (2 * MathF.PI) / p)
                + c
                + b;
            }

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                if(Mathf.Approximately(t,0)) return b;

                t /= d * f0_5;

                if(Mathf.Approximately(t, 2)) return b + c;

                float p = d * (f0_3 * f1_5);
                float s = p * f0_25;


                if(t < 1)
                {
                    t -= 1;

                    return-f0_5 * (c * MathF.Pow(2, 10 * t) * MathF.Sin((t * d - s) * (2 * MathF.PI) / p)) + b;
                }
            
                t -= 1;

                return c * MathF.Pow(2, -10 * t) * MathF.Sin((t * d - s) * (2 * MathF.PI) / p) * f0_5 + c + b;
            }
        }

        private static class Expo
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                if(Mathf.Approximately(t, 0)) return b;

                return c * MathF.Pow(2, 10 * (t / d - 1)) + b;
            }

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
                => Mathf.Approximately(t, d) ? b + c : c * (-MathF.Pow(2, -10 * t / d) + 1) + b;

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                if(Mathf.Approximately(t,0)) return b;
                if(Mathf.Approximately(t, d)) return b + c;

                t /= d * f0_5;

                if(t < 1) return c * f0_5 * MathF.Pow(2, 10 * (t - 1)) + b;

                -- t;

                return c * f0_5 * (-MathF.Pow(2, -10 * t) + 2) + b;
            }
        }

        private static class Linear
        {
            public static float EaseNone
            (
                float t,
                float b,
                float c,
                float d
            )
                => c * t / d + b;
        }

        private static class Quad
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d;

                return c * t * t + b;
            }

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d;

                return-c * t * (t - 2) + b;
            }

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d * f0_5;
            
                if(t < 1) return c * f0_5 * (t * t) + b;

                -- t;

                return -c * f0_5 * ((t - 2) * t - 1) + b;
            }
        }

        private static class Quart
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d;

                return c * t * t * t * t + b;
            }

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t = t / d - 1;

                return-c * (t * t * t * t - 1) + b;
            }

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d * f0_5;

                if(t < 1) return c / 2 * t * t * t * t + b;

                t -= 2;

                return-c * f0_5 * (t * t * t * t - 2) + b;
            }
        }

        private static class Quint
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d;

                return c * t * t * t * t * t + b;
            }

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t = t / d - 1;

                return c * (t * t * t * t * t + 1) + b;
            }

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
            {
                t /= d * f0_5;

                if(t < 1) return c * f0_5 * t * t * t * t * t + b;

                t -= 2;

                return c  * f0_5 * (t * t * t * t * t + 2) + b;
            }
        }

        private static class Sine
        {
            public static float EaseIn
            (
                float t,
                float b,
                float c,
                float d
            )
                => -c * MathF.Cos(t / d * (MathF.PI * f0_5)) + c + b;

            public static float EaseOut
            (
                float t,
                float b,
                float c,
                float d
            )
                => c * MathF.Sin(t / d * (MathF.PI * f0_5)) + b;

            public static float EaseInOut
            (
                float t,
                float b,
                float c,
                float d
            )
                => -c * f0_5 * (MathF.Cos(MathF.PI * t / d) - 1) + b;
        }

        public static float GetValueOnCurve
        (
            float curveStartVal,
            float curveEndVal,
            float currVal,
            IceFrostEases iceFrostEase
        )
        {
            float amount = curveEndVal - curveStartVal;

            return iceFrostEase switch
            {
                IceFrostEases.BackIn => Back.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.BackOut => Back.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.BackInOut => Back.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.BounceIn => Bounce.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.BounceOut => Bounce.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.BounceInOut => Bounce.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.CircIn => Circ.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.CircOut => Circ.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.CircInOut => Circ.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.CubicIn => Cubic.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.CubicOut => Cubic.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.CubicInOut => Cubic.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.ElasticIn => Elastic.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.ElasticOut => Elastic.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.ElasticInOut => Elastic.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.ExpoIn => Expo.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.ExpoOut => Expo.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.ExpoInOut => Expo.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.LinearIn => Linear.EaseNone
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.LinearOut => Linear.EaseNone
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.LinearInOut => Linear.EaseNone
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuadIn => Quad.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuadOut => Quad.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuadInOut => Quad.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuartIn => Quart.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuartOut => Quart.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuartInOut => Quart.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuintIn => Quint.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuintOut => Quint.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.QuintInOut => Quint.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.SineIn => Sine.EaseIn
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.SineOut => Sine.EaseOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                IceFrostEases.SineInOut => Sine.EaseInOut
                (
                    currVal,
                    curveStartVal,
                    amount,
                    1
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(iceFrostEase), iceFrostEase, null)
            };
        }

        public enum IceFrostEases
        {
            BackIn,
            BackOut,
            BackInOut,
            BounceIn,
            BounceOut,
            BounceInOut,
            CircIn,
            CircOut,
            CircInOut,
            CubicIn,
            CubicOut,
            CubicInOut,
            ElasticIn,
            ElasticOut,
            ElasticInOut,
            ExpoIn,
            ExpoOut,
            ExpoInOut,
            LinearIn,
            LinearOut,
            LinearInOut,
            QuadIn,
            QuadOut,
            QuadInOut,
            QuartIn,
            QuartOut,
            QuartInOut,
            QuintIn,
            QuintOut,
            QuintInOut,
            SineIn,
            SineOut,
            SineInOut
        }
    }
}