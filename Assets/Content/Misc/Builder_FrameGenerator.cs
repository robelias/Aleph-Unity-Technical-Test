using UnityEngine;

namespace Content.Misc
{
    public static class Builder_FrameGenerator
    {
        private static GameObject _artObject;
        private static Material _frameMaterial;
        private static GameObject _topFrame, _bottomFrame, _leftFrame, _rightFrame;
        private const float FRAME_THICKNESS = 5f;

        public static void GenerateFrameAndCanvas(float widthCm, float heightCm, GameObject artParent, Material material, Transform detailTarget, out Transform artCanvas)
        {
            _artObject = artParent;
            _frameMaterial = material;
            var widthUnity = DimensionConverter.CmToUnityUnits(widthCm);
            var heightUnity = DimensionConverter.CmToUnityUnits(heightCm);
            var frameThicknessUnity = DimensionConverter.CmToUnityUnits(FRAME_THICKNESS);

            artCanvas = CreateCanvas( widthUnity, heightUnity, detailTarget);
            GenerateFrame(widthUnity, heightUnity, frameThicknessUnity);
        }

        private static Transform CreateCanvas(float width, float height, Transform placedObject)
        {
            var canvas = GameObject.CreatePrimitive(PrimitiveType.Quad);
            canvas.transform.parent = _artObject.transform;
            canvas.transform.localScale = new Vector3(width, height, 1);
            canvas.transform.localPosition = Vector3.zero;
            canvas.transform.localRotation = Quaternion.Euler(0, 180, 0);

            _artObject.GetComponent<BoxCollider>().size = new Vector3(width, height, 0.5f);
            _artObject.GetComponent<BoxCollider>().center = Vector3.zero;
            
            Vector3 position = new Vector3(-width/2, -height/2, 0);
            placedObject.localPosition =  position;
            
            return canvas.transform;
        }

        private static void GenerateFrame(float width, float height, float frameThickness)
        {
            //Top
            _topFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _topFrame.transform.parent = _artObject.transform;
            _topFrame.transform.localScale = new Vector3(width + frameThickness * 2, frameThickness, frameThickness);
            _topFrame.transform.localPosition = new Vector3(0, height / 2 + frameThickness / 2, 0);
            _topFrame.transform.localRotation = Quaternion.identity;
            _topFrame.GetComponent<Renderer>().material = _frameMaterial;

            //Botton
            _bottomFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _bottomFrame.transform.parent = _artObject.transform;
            _bottomFrame.transform.localScale = new Vector3(width + frameThickness * 2, frameThickness, frameThickness);
            _bottomFrame.transform.localPosition = new Vector3(0, -height / 2 - frameThickness / 2, 0);
            _bottomFrame.transform.localRotation = Quaternion.identity;
            _bottomFrame.GetComponent<Renderer>().material = _frameMaterial;

            //Left
            _leftFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _leftFrame.transform.parent = _artObject.transform;
            _leftFrame.transform.localScale = new Vector3(frameThickness, height, frameThickness);
            _leftFrame.transform.localPosition = new Vector3(-width / 2 - frameThickness / 2, 0, 0);
            _leftFrame.transform.localRotation = Quaternion.identity;
            _leftFrame.GetComponent<Renderer>().material = _frameMaterial;

            //Right
            _rightFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _rightFrame.transform.parent = _artObject.transform;
            _rightFrame.transform.localScale = new Vector3(frameThickness, height, frameThickness);
            _rightFrame.transform.localPosition = new Vector3(width / 2 + frameThickness / 2, 0, 0);
            _rightFrame.transform.localRotation = Quaternion.identity;
            _rightFrame.GetComponent<Renderer>().material = _frameMaterial;
        }
    }
}