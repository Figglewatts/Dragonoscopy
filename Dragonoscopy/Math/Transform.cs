using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Dragonoscopy.Math
{
    public class Transform
    {
        private bool _dirty;

        public Transform Parent { get; set; }

        private Vector2 _position;
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                _dirty = true;
            }
        }

        private Vector2 _scale;
        public Vector2 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _dirty = true;
            }
        }

        private float _angle;
        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                _dirty = true;
            }
        }

        private Matrix _matrix;
        public Matrix Matrix
        {
            get
            {
                if (_dirty) recomputeMatrix();
                if (Parent == null)
                {
                    return _matrix;
                }
                return _matrix * Parent.Matrix;
            }
            private set => _matrix = value;
        }

        private Matrix _inverseMatrix;
        public Matrix InverseMatrix
        {
            get
            {
                if (_dirty) recomputeMatrix();
                return _inverseMatrix;
            }
            private set => _inverseMatrix = value;
        }

        public Vector3 Up => (this.Matrix.Up);
        public Vector3 Right => (this.Matrix.Right);

        public Transform()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Angle = 0f;
            Matrix = Matrix.Identity;
            InverseMatrix = Matrix.Identity;
            Parent = null;
        }

        public Vector3 ToWorld(Vector3 pos)
        {
            return Vector3.Transform(pos, Matrix);
        }

        public Vector3 ToLocal(Vector3 pos)
        {
            return Vector3.Transform(pos, InverseMatrix);
        }

        public Transform Translate(Vector2 v)
        {
            Matrix = Matrix.CreateTranslation(new Vector3(v, 0)) * _matrix;
            InverseMatrix = Matrix.CreateTranslation(new Vector3(-v, 0)) * InverseMatrix;
            _position += v;
            return this;
        }

        public Transform Rescale(Vector2 s)
        {
            Matrix = Matrix.CreateScale(new Vector3(s, 1)) * _matrix;
            InverseMatrix = Matrix.CreateScale(Vector3.One) * InverseMatrix;
            _scale *= s;
            return this;
        }

        public Transform Rotate(float angle)
        {
            Matrix = Matrix.CreateFromAxisAngle(Vector3.Forward, angle) * _matrix;
            InverseMatrix = Matrix.CreateFromAxisAngle(Vector3.Forward, -angle) * InverseMatrix;
            _angle += angle;
            return this;
        }

        private void recomputeMatrix()
        {
            _matrix = Matrix.CreateFromAxisAngle(Vector3.Forward, _angle) * Matrix.CreateScale(new Vector3(_scale, 1)) *
                      Matrix.CreateTranslation(new Vector3(_position, 0));
            InverseMatrix = Matrix.CreateFromAxisAngle(Vector3.Forward, -_angle) * Matrix.CreateScale(Vector3.One) *
                            Matrix.CreateTranslation(-new Vector3(_position, 0));
        }
    }
}
