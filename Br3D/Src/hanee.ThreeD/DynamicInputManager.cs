using devDept.Geometry;
using DevExpress.Utils;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class DynamicInputManager
    {
        public enum Point3DType
        {
            xyz,
            distanceAngle,
            distanceFactor
        }
        
        static public devDept.Eyeshot.Environment environment { get; set; }
        static FormXyzDynamicInput formXyzDynamicInput;
        static FormDistanceAngleDynamicInput formDistanceAngleDynamicInput;
        static FormDistanceFactorDynamicInput formDistanceFactorDynamicInput;
        static public bool disableFlagDynamicInput { get; set; } = false;
        static public Point3DType point3DType { get; set; } = Point3DType.xyz;
        static SvgImageCollection svgImageCollection { get; set; }
        static public System.Drawing.Brush foreBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        // 이미지 콜렉션
        // 
        static public SvgImageCollection GetSvgImageCollection()
        {
            if(svgImageCollection == null)
            {
                svgImageCollection = new SvgImageCollection();
                svgImageCollection.Add("unlock", "image://svgimages/actions/cleartablestyle.svg");
                svgImageCollection.Add("lock", "image://svgimages/outlook inspired/private.svg");
            }

            return svgImageCollection;
        }

        public static void Init()
        {
            point3DType = Point3DType.xyz;
        }

        // lock, unlock 아이콘을 그린다.
        static public void DrawLayoutControl(ref ItemCustomDrawEventArgs e, string title, int imageIdx)
        {
            e.Cache.DrawImage(DynamicInputManager.GetSvgImageCollection().GetImage(imageIdx), new System.Drawing.Point(e.Bounds.X+4, e.Bounds.Y+4));
            e.Cache.DrawString(title, Control.DefaultFont, DynamicInputManager.foreBrush, e.Bounds.X + 20, e.Bounds.Y + 7);
            e.Handled = true;
        }

        // 현재 사용중인 dynamic input form을 리턴
        static public Form GetFormPoint3DDynamicInput()
        {
            // 객체 선택중일때는 form을 리턴하지 않는다.
            if (ActionBase.IsSelectingEntity())
                return null;

            if (point3DType == Point3DType.xyz)
            {
                if (formXyzDynamicInput == null)
                {
                    formXyzDynamicInput = new FormXyzDynamicInput();
                    formXyzDynamicInput.TopMost = true;
                }
                return formXyzDynamicInput;
            }
            else if (point3DType == Point3DType.distanceAngle)
            {
                if (formDistanceAngleDynamicInput == null)
                {
                    formDistanceAngleDynamicInput = new FormDistanceAngleDynamicInput();
                    formXyzDynamicInput.TopMost = true;
                }
                return formDistanceAngleDynamicInput;
            }
            else if (point3DType == Point3DType.distanceFactor)
            {
                if (formDistanceFactorDynamicInput == null)
                {
                    formDistanceFactorDynamicInput = new FormDistanceFactorDynamicInput();
                    formDistanceFactorDynamicInput.TopMost = true;
                }
                return formDistanceFactorDynamicInput;
            }

            return null;
        }

        static public void ShowDynamicInput(devDept.Eyeshot.Environment environment)
        {
            DynamicInputManager.environment = environment;
            var form = GetFormPoint3DDynamicInput();
            if (form == null)
                return;

            IDynamicInput di = form as IDynamicInput;

            if (!form.Visible)
            {
                form.Visible = true;

                // 숨김상태에서 처음 보여지게 되면 init을 한다.
                if (di != null)
                    di.Init();
            }

            var loc = Cursor.Position;
            loc.X += 50;
            form.Location = loc;
            if (di != null)
                di.UpdateControls(environment);

            // main form이 항상 focus 되어 있도록 해야 자연스러움
            var parentForm = environment.FindForm();
            if (parentForm  != null)
            {
                parentForm.Focus();
            }
        }

        // length input을 활성화 한다.
        public static void ActiveLengthFactor(Point3D startPoint, double baseLength, string title = null)
        {
            DynamicInputManager.point3DType = DynamicInputManager.Point3DType.distanceFactor;
            var formDi = DynamicInputManager.GetFormPoint3DDynamicInput() as FormDistanceFactorDynamicInput;
            if (formDi == null)
                return;

            
            formDi.Show();

            formDi.fixedFactor = null;
            formDi.baseLength = baseLength;
            if (title != null)
            {
                formDi.LayoutControlItemFactor.Text = title;
            }
            

            HModel hModel = environment as HModel;
            if (hModel == null)
                return;

            hModel.orthoModeManager.startPoint = startPoint;
        }
        // length and angle input을 활성화 한다.
        public static void ActiveLengthAndAngle(Point3D startPoint, double? fixedAngle = null, double ?fixedLength=null)
        {
            DynamicInputManager.point3DType = DynamicInputManager.Point3DType.distanceAngle;
            var formDi = DynamicInputManager.GetFormPoint3DDynamicInput() as FormDistanceAngleDynamicInput;
            if (formDi == null)
                return;
            
            formDi.Show();

            // show를 하고 나서 fixed값을 설정해야함(show할때 fixed 값을 초기화 하기 때문)
            formDi.fixedAngle = fixedAngle;
            formDi.fixedLength = fixedLength;
            if (formDi.fixedLength != null)
            {
                formDi.TextEditAngle.Focus();
                formDi.TextEditAngle.SelectAll();
            }

            HModel hModel = environment as HModel;
            if (hModel == null)
                return;

            hModel.orthoModeManager.startPoint = startPoint;
        }

        static public void HideDynamicInput()
        {
            var form = GetFormPoint3DDynamicInput();
            if (form == null)
                return;
            if (form.Visible)
                form.Visible = false;
        }

        internal static void OnKeyUp(KeyEventArgs e)
        {
            var form = GetFormPoint3DDynamicInput();
            if (form == null ||
                form.Visible == false)
                return;
            form.Focus();
        }

        internal static void FlagPoint3DType()
        {
            if (disableFlagDynamicInput)
                return;

            HideDynamicInput();
            if (point3DType == Point3DType.xyz)
                point3DType = Point3DType.distanceAngle;
            else
                point3DType = Point3DType.xyz;
        }

        // dynamic input에서 fixed 된거 고려해서 좌표 조정
        internal static void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt)
        {
            var form = GetFormPoint3DDynamicInput();
            if (form == null || !form.Visible)
                return;

            var dynamicInput = form as IDynamicInputPoint3D;
            if (dynamicInput == null)
                return;
            
            dynamicInput.ModifyPoint3D(environment, ref pt);
        }
    }
}
