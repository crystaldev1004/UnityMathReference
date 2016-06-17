﻿using System.Collections.Generic;

namespace Reign
{
	public struct BoundingBox2
	{
		#region Properties
		public Vec2 min, max;

		public float left
		{
			get {return min.x;}
			set {min.x = value;}
		}

		public float bottom
		{
			get {return min.y;}
			set {min.y = value;}
		}

		public float right
		{
			get {return max.x;}
			set {max.x = value;	}
		}

		public float top
		{
			get {return max.y;}
			set {max.y = value;}
		}

		public static readonly BoundingBox2 zero = new BoundingBox2();
		#endregion

		#region Constructors
		public BoundingBox2(Vec2 min, Vec2 max)
        {
            this.min = min;
            this.max = max;
        }

		public static BoundingBox2 FromPoints(IList<Vec2> points)
        {
            BoundingBox2 boundingBox;
            boundingBox.min = points[0];
            boundingBox.max = boundingBox.min;
			foreach (var point in points)
            {
                if (point.x < boundingBox.min.x)
				{
                    boundingBox.min.x = point.x;
				}
                else if (point.x > boundingBox.max.x)
				{
                    boundingBox.max.x = point.x;
				}

                if (point.y < boundingBox.min.y)
				{
                    boundingBox.min.y = point.y;
				}
                else if (point.y > boundingBox.max.y)
				{
                    boundingBox.max.y = point.y;
				}
            }

            return boundingBox;
        }
		#endregion

		#region Methods
		public bool Intersects(BoundingBox2 boundingBox)
        {
            return
				!(boundingBox.min.x > max.x || boundingBox.min.y > max.y ||
				  min.x > boundingBox.max.x || min.y > boundingBox.max.y);
        }

		public bool Intersects(BoundingSphere2 boundingSphere)
        {
		   Vec2 clampedLocation;
            if (boundingSphere.center.x > max.x)
			{
                clampedLocation.x = max.x;
			}
            else if (boundingSphere.center.x < min.x)
			{
                clampedLocation.x = min.x;
			}
            else
			{
                clampedLocation.x = boundingSphere.center.x;
			}

            if (boundingSphere.center.y > max.y)
			{
                clampedLocation.y = max.y;
			}
            else if (boundingSphere.center.y < min.y)
			{
                clampedLocation.y = min.y;
			}
            else
			{
                clampedLocation.y = boundingSphere.center.y;
			}

            return clampedLocation.DistanceSquared(boundingSphere.center) <= (boundingSphere.radius * boundingSphere.radius);
        }

		public BoundingBox2 Merge(BoundingBox2 boundingBox2)
        {
			BoundingBox2 result;
            if (min.x < boundingBox2.min.x) result.min.x = min.x;
            else result.min.x = boundingBox2.min.x;

            if (min.y < boundingBox2.min.y) result.min.y = min.y;
            else result.min.y = boundingBox2.min.y;

            if (max.x > boundingBox2.max.x) result.max.x = max.x;
            else result.max.x = boundingBox2.max.x;

            if (max.y > boundingBox2.max.y) result.max.y = max.y;
            else result.max.y = boundingBox2.max.y;

			return result;
        }
		#endregion
	}
}