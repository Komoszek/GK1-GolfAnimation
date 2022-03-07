using GK1_GolfAnimation.Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GK1_GolfAnimation.Models;
using System.Drawing;

namespace GK1_GolfAnimation
{
    class Scene
    {
        public List<Light> lights;
        public List<SceneObject> sceneObjects;
        public View view;
        public Projection projection;

        public SceneObject ball;

        public BallAnimation ballAnimation;
        public Color Ia;

        public Fog fog = null;

        public Scene()
        {
            lights = new List<Light>();
            sceneObjects = new List<SceneObject>();
            view = new View();
            this.Ia = Color.White;

            view.moveTo(new Vector3(0, 20, 0));
            view.lookAt(new Vector3(0, 0, 0));

            projection = new Projection();

            Model3D model;
            Color[] tempArr;

            lights.Add(new SpotLight(new Vector3(-5, -5, 6), -Vector3.UnitZ, Color.Magenta, 20));
            lights.Add(new SpotLight(new Vector3(5, 5, 6), -Vector3.UnitZ, Color.Cyan, 20));

            model = new Ball();
            tempArr = new Color[model.triangles.Count];

            // golf Ball
            for (int i = 0; i < tempArr.Length; i++)
            {
                tempArr[i] = i % 2 == 0 ? Color.Blue : Color.Green;
            }

            float ballscale = 0.3f;

            sceneObjects.Add(new SceneObject(model, new Vector3(0, 5, ballscale), tempArr, Vector3.Zero, new Vector3(ballscale, ballscale, ballscale)));

            ball = sceneObjects[sceneObjects.Count - 1];

            tempArr = new Color[model.triangles.Count];

            // reflector balls
            for (int i = 0; i < tempArr.Length; i++)
            {
                tempArr[i] = Color.Magenta;
            }

            sceneObjects.Add(new SceneObject(model, new Vector3(-5, -5, 0), tempArr, Vector3.Zero, Vector3.One));
            tempArr = new Color[model.triangles.Count];

            // reflector balls
            for (int i = 0; i < tempArr.Length; i++)
            {
                tempArr[i] = Color.Cyan;
            }
            sceneObjects.Add(new SceneObject(model, new Vector3(5, 5, 0), tempArr, Vector3.Zero, Vector3.One));

            model = new GolfHill();
            float slope = 2.0f;
            
            tempArr = new Color[model.triangles.Count];
            tempArr[0] = Color.Green;
            tempArr[1] = Color.Green;
            for (int i = 2; i < tempArr.Length; i++)
            {
                tempArr[i] = Color.Brown;
            }

            sceneObjects.Add(new SceneObject(model, Vector3.Zero, tempArr, Vector3.Zero, Vector3.One));
            
            model = new Ground();
            tempArr = new Color[model.triangles.Count];
            int l = 20 * 20 * 2;
            for(int i = 0; i < l; i++)
            {
                tempArr[i] = Color.Gray;
            }
            for (int i = l; i < tempArr.Length; i++)
            {
                tempArr[i] = Color.Brown;
            }

            sceneObjects.Add(new SceneObject(model, Vector3.Zero, tempArr, Vector3.Zero, Vector3.One));

            model = new Cylinder();
            tempArr = new Color[model.triangles.Count];
            for (int i = 0; i < tempArr.Length; i++)
            {
                tempArr[i] = Color.Gray;
            }

            sceneObjects.Add(new SceneObject(model, new Vector3(0, 0, 3f), tempArr, Vector3.Zero, new Vector3(0.1f, 0.1f, 2f)));


            SpotLight ballLight = new SpotLight(ball.Position, Vector3.Normalize(-ball.Position), Color.White, 4);
            lights.Add(ballLight);



            ballAnimation = new BallAnimation(7312, ball.Position, ball.Position.Z, 0.8f, 
                                                slope * 2, 1 / (slope - 1), 6.0f, 1.0f, 2.0f, 4.0f, ballLight);
            fog = new Fog(Color.Gray,  0.0f);
        }
    }
}
