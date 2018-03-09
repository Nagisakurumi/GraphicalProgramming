using MyXObject;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyXAribute
{
    /// <summary>
    /// 贝塞尔曲线
    /// </summary>
    public class BezierLine : IDisposable
    {
        #region 自定义属性
        /// <summary>
        /// 标示符
        /// </summary>
        private int _id;
        /// <summary>
        /// 提供曲线形状的控件可测试区域
        /// </summary>
        Path connector = new Path();
        /// <summary>
        /// 表示有各种图形组成的复杂形状
        /// </summary>
        private PathGeometry connectorGeometry;
        /// <summary>
        /// 表示由各种元素组成的复杂路劲
        /// </summary>
        private PathFigure connectorPoints;
        /// <summary>
        /// 构建一个三次方的贝塞尔曲线
        /// </summary>
        private BezierSegment connectorCurve;
        /// <summary>
        /// 笔触的大小
        /// </summary>
        private double _strokeThickness = 5;
        /// <summary>
        /// 起始点
        /// </summary>
        private BezierPoint _startPoint = new BezierPoint();
        /// <summary>
        /// 最大弯曲距离
        /// </summary>
        private double maxdis = 300;
        /// <summary>
        /// 最小距离
        /// </summary>
        private double mindis = 10;
        /// <summary>
        /// 结束点
        /// </summary>
        private BezierPoint _endPoint = new BezierPoint();
        /// <summary>
        /// 弯曲率
        /// </summary>
        private static float BendLow = 1.1f;
        #endregion
        #region 读取器
        /// <summary>
        /// 笔触大小读取器
        /// </summary>
        public double StrokeThickness
        {
            get
            {
                return _strokeThickness;
            }

            set
            {
                _strokeThickness = value;
                connector.StrokeThickness = value;
            }
        }
        /// <summary>
        /// 贝塞尔曲线对象
        /// </summary>
        public Path Bezier
        {
            get
            {
                return connector;
            }
        }
        /// <summary>
        /// 标示符
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                //connector.Name = value;
            }
        }
        /// <summary>
        /// 起始点
        /// </summary>
        public BezierPoint StartPoint
        {
            get
            {
                return _startPoint;
            }

            set
            {
                _startPoint = value;
            }
        }
        /// <summary>
        /// 结束点
        /// </summary>
        public BezierPoint EndPoint
        {
            get
            {
                return _endPoint;
            }

            set
            {
                _endPoint = value;
            }
        }
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="LineColor">线条的颜色</param>
        /// <param name="onePoint">坐标</param>
        /// <param name="position">位置信息</param>
        public BezierLine(int id, Color LineColor, Point onePoint, XAribute.XPositonStyle position)
        {
            ///赋予笔刷
            connector.Stroke = new SolidColorBrush(LineColor);
            ///赋予笔触
            connector.StrokeThickness = _strokeThickness;
            ///实例化
            connectorGeometry = new PathGeometry();
            connectorPoints = new PathFigure();
            connectorCurve = new BezierSegment();
            StartPoint.position = onePoint;
            StartPoint.positionType = position;
            EndPoint.position = onePoint;
            
            ///设置贝塞尔曲线的开始点
            connectorPoints.StartPoint = StartPoint.position;
            SetBezierLine(EndPoint.position, position);
            connectorPoints.Segments.Add(connectorCurve);
            connectorGeometry.Figures.Add(connectorPoints);
            connector.Data = connectorGeometry;

            this.Id = id;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="LineColor">线条的颜色</param>
        /// <param name="onePoint">坐标</param>
        /// <param name="position">位置信息</param>
        public BezierLine(int id)
        {
            this.Id = id;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~BezierLine()
        {
            Dispose();
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 在已经设置信息的情况下初始化
        /// </summary>
        protected void InitLineInformation()
        {
            ///赋予笔刷
            connector.Stroke = new SolidColorBrush(StartPoint.LinkAribute.BorderColor);
            ///赋予笔触
            connector.StrokeThickness = _strokeThickness;
            ///实例化
            connectorGeometry = new PathGeometry();
            connectorPoints = new PathFigure();
            connectorCurve = new BezierSegment();

            ///设置贝塞尔曲线的开始点
            connectorPoints.StartPoint = StartPoint.position;
            AdjustBezierLine();

            connectorPoints.Segments.Add(connectorCurve);
            connectorGeometry.Figures.Add(connectorPoints);
            connector.Data = connectorGeometry;
        }
        /// <summary>
        /// 设置贝塞尔曲线的控制点
        /// </summary>
        /// <param name="EndPoint">结束点</param>
        private void SetBezierOver(int pos,Point OverPoint)
        {           
            //switch(pos)
            //{
            //    case 1:
            //        connectorCurve.Point1 = new Point(StartPoint.X + (StartPoint.X + OverPoint.X)/10, StartPoint.Y);
            //        connectorCurve.Point2 = new Point(OverPoint.X - (StartPoint.X + OverPoint.X) / 10, OverPoint.Y);
            //        connectorCurve.Point3 = OverPoint;
            //        break;
            //    case 2:
            //        connectorCurve.Point1 = new Point(StartPoint.X - (StartPoint.X + OverPoint.X) / 10, StartPoint.Y);
            //        connectorCurve.Point2 = new Point(OverPoint.X + (StartPoint.X + OverPoint.X) / 10, OverPoint.Y);
            //        connectorCurve.Point3 = OverPoint;
            //        break;
            //    case 3:
            //        connectorCurve.Point1 = new Point(StartPoint.X - (StartPoint.X + OverPoint.X) / 10, StartPoint.Y);
            //        connectorCurve.Point2 = new Point(OverPoint.X + (StartPoint.X + OverPoint.X) / 10, OverPoint.Y);
            //        connectorCurve.Point3 = OverPoint;
            //        break;
            //    case 4:
            //        connectorCurve.Point1 = new Point(StartPoint.X + (StartPoint.X + OverPoint.X) / 10, StartPoint.Y);
            //        connectorCurve.Point2 = new Point(OverPoint.X - (StartPoint.X + OverPoint.X) / 10, OverPoint.Y);
            //        connectorCurve.Point3 = OverPoint;
            //        break;
            //}
        }
        /// <summary>
        /// 设置贝塞尔曲线的控制点
        /// </summary>
        /// <param name="EndPoint">结束点</param>
        private void SetBezierOver(XAribute.XPositonStyle position, Point OverPoint)
        {            
            EndPoint.position = OverPoint;
            EndPoint.positionType = position;
            AdjustBezierLine();
        }
        /// <summary>
        /// 调整贝塞尔曲线的位置
        /// </summary>
        /// <param name="position">结尾点位置信息</param>
        public void AdjustBezierLine()
        {
            ///起始点
            Point startp = StartPoint.LinkAribute == null ? StartPoint.position : StartPoint.LinkAribute.GetWorldPosition();
            ///终止点
            Point endp = EndPoint.LinkAribute == null ? EndPoint.position : EndPoint.LinkAribute.GetWorldPosition();
            ///弯曲度
            double cury = Math.Abs(startp.Y - endp.Y) / BendLow;
            double curx = 0;
            ///需要的情况下才启用X方向的取值
            if(StartPoint.LinkAribute != null && StartPoint.LinkAribute.SelectPositionStyle == XAribute.XPositonStyle.Left && (startp.X - endp.X) < 0)
            {
                curx = Math.Abs(startp.X - endp.X) / BendLow;
            }
            else if(StartPoint.LinkAribute != null && StartPoint.LinkAribute.SelectPositionStyle == XAribute.XPositonStyle.right && (startp.X - endp.X) > 0)
            {
                curx = Math.Abs(startp.X - endp.X) / BendLow;
            }
            double cur = curx > cury ? curx : cury;
            ///弯曲度
            double dis = cur > maxdis ? maxdis : cur;            
            dis = dis < mindis ? mindis : dis;
            switch (EndPoint.positionType)
            {
                case XAribute.XPositonStyle.Left:
                    connectorPoints.StartPoint = startp;
                    connectorCurve.Point1 = new Point(startp.X + dis, startp.Y);
                    connectorCurve.Point2 = new Point(endp.X - dis, endp.Y);
                    connectorCurve.Point3 = endp;
                    break;
                case XAribute.XPositonStyle.right:
                    connectorPoints.StartPoint = startp;
                    connectorCurve.Point1 = new Point(startp.X - dis, startp.Y);
                    connectorCurve.Point2 = new Point(endp.X + dis, endp.Y);
                    connectorCurve.Point3 = endp;
                    break;
            }
        }
        /// <summary>
        /// 设置贝塞尔曲线
        /// </summary>
        /// <param name="OverPoint">贝塞尔曲线的结束的点</param>
        public void SetBezierLine(Point OverPoint, XAribute.XPositonStyle position)
        {
            #region MyRegion
            //if (StartPoint != null)
            //{
            //    if (OverPoint.X >= StartPoint.X && OverPoint.Y >= StartPoint.Y)
            //    {
            //        SetBezierOver(1, OverPoint);
            //    }
            //    else if (OverPoint.X <= StartPoint.X && OverPoint.Y >= StartPoint.Y)
            //    {
            //        SetBezierOver(2, OverPoint);
            //    }
            //    else if (OverPoint.X <= StartPoint.X && OverPoint.Y >= StartPoint.Y)
            //    {
            //        SetBezierOver(3, OverPoint);
            //    }
            //    else if (OverPoint.X >= StartPoint.X && OverPoint.Y <= StartPoint.Y)
            //    {
            //        SetBezierOver(4, OverPoint);
            //    }
            //}     
            #endregion
            SetBezierOver(position, OverPoint);           
        }
        /// <summary>
        /// 直接设置贝塞尔曲线的2个点
        /// </summary>
        /// <param name="start">起始位置</param>
        /// <param name="end">终止位置</param>
        public void DirectSetBezierLineTwoPoint(XAribute start, XAribute end)
        {
            StartPoint.LinkAribute = start;
            StartPoint.position = start.GetWorldPosition();
            StartPoint.positionType = start.SelectPositionStyle;
            EndPoint.LinkAribute = end;
            EndPoint.position = end.GetWorldPosition();
            EndPoint.positionType = end.SelectPositionStyle;

            InitLineInformation();
        }
        /// <summary>
        /// 当父控件移动的时候调整贝塞尔曲线的位置(弃用的方法)
        /// </summary>
        /// <param name="position">点的坐标</param>
        /// <param name="dirtype">属性的方向类型</param>
        public void BezierPositionChange(Point position, XAribute.XPositonStyle dirtype)
        {
            AdjustBezierLine();
            //if (dirtype == StartPoint.positionType)
            //{
            //    SetBezierStart(dirtype, position);
            //}
            //else
            //{
            //    SetBezierOver(dirtype, position);
            //}
        }
        /// <summary>
        /// 设置的是起始点(弃用的方法)
        /// </summary>
        /// <param name="position">位置类型</param>
        /// <param name="fistPoint">点的坐标</param>
        protected void SetBezierStart(XAribute.XPositonStyle position, Point fistPoint)
        {
            double cur = (fistPoint.Y - EndPoint.position.Y) / ToolHelp.DisPoint(fistPoint, EndPoint.position) / 7;
            double dis = cur > maxdis ? maxdis : cur;
            dis = dis < mindis ? mindis : dis;
            StartPoint.position = fistPoint;
            switch (position)
            {
                case XAribute.XPositonStyle.Left:
                    connectorPoints.StartPoint = StartPoint.position;
                    connectorCurve.Point1 = new Point(StartPoint.position.X - dis, StartPoint.position.Y);
                    connectorCurve.Point2 = new Point(EndPoint.position.X + dis, EndPoint.position.Y);
                    //connectorCurve.Point3 = OverPoint;
                    break;
                case XAribute.XPositonStyle.right:
                    connectorPoints.StartPoint = StartPoint.position;
                    connectorCurve.Point1 = new Point(StartPoint.position.X + dis, StartPoint.position.Y);
                    connectorCurve.Point2 = new Point(EndPoint.position.X - dis, EndPoint.position.Y);
                    //connectorCurve.Point3 = OverPoint;
                    break;
            }
        }
        /// <summary>
        /// 删除贝塞尔曲线的时候进行必要的操作
        /// </summary>
        public void DelBezierLine()
        {
            StartPoint.LinkAribute.ChildRemoveBezierLine(this);
            EndPoint.LinkAribute.ChildRemoveBezierLine(this);
            StartPoint = null;
            EndPoint = null;
        }
        /// <summary>
        /// 清理资源
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //执行基本的清理代码
                
            }
        }
        #endregion
    }
    /// <summary>
    /// 关于贝塞尔曲线的起点和终点
    /// </summary>
    public class BezierPoint
    {
        /// <summary>
        /// 点的坐标
        /// </summary>
        public Point position = new Point();
        /// <summary>
        /// 点对于属性的类型
        /// </summary>
        public XAribute.XPositonStyle positionType;
        /// <summary>
        /// 连接的属性
        /// </summary>
        public XAribute LinkAribute;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BezierPoint()
        {
            positionType = XAribute.XPositonStyle.Left;
        }
    }
}
