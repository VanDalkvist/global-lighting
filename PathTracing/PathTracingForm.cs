using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using GlobalLighting.Core;
using GlobalLighting.Lights;
using GlobalLighting.Material;
using GlobalLighting.Surfaces;

namespace PathTracing
{
	public partial class PathTracingForm : Form
	{
		public PathTracingForm()
		{
			InitializeComponent();

			IMaterial diffuseMaterial = new DiffuseMaterial(new Radiance(1));
			IMaterial diffuseMaterialYellow = new DiffuseMaterial(new Radiance(1, 1, 0));
			IMaterial diffuseMaterialPurple = new DiffuseMaterial(new Radiance(1, 0, 1));
			IMaterial diffuseMaterialGreen = new DiffuseMaterial(new Radiance(0, 1, 0));
			IMaterial specularMaterialRed = new SpecularMaterial(new Radiance(1, 0, 0), 100);
			IMaterial diffuseMaterialRed = new DiffuseMaterial(new Radiance(1, 0, 0));
			IMaterial specularMaterial = new SpecularMaterial(new Radiance(1), 500);
			IMaterial diffuseMaterialBlue = new DiffuseMaterial(new Radiance(0, 0, 1));

			SquareLight light = new SquareLight(
				new Vector(-0.15, 0.5 - 0.01, 1.35),
				new Vector(0.15, 0.5 - 0.01, 1.35),
				new Vector(-0.15, 0.5 - 0.01, 1.65),
				new Radiance(50));

			Scene room = new Scene(

				new Room(new Size3D(0.5, 0.5, 0.5), 1, new Room.Materials()),
				new Table(new Vector(0, -0.2875, 1.5), new Size3D(0.3, 0.0125, 0.3), 0.05, diffuseMaterialGreen, diffuseMaterialYellow),

				new Sphere(0.15, new Vector(0.35, 0.35, 1.85), diffuseMaterialBlue),
				new Sphere(0.2, new Vector(-0.3, 0.3, 1.8), specularMaterial),
				new Sphere(0.1, new Vector(0, -0.4, 1.5), diffuseMaterialRed)
				);

			int countOfFrames = 3;
			int countOfShadowRays = 1;
			double absorption = 0.4;
			int countOfPixelsOnHorizontal = 480;
			int countOfPixelsOnVertical = 480;

			BitmapPanel.Width = countOfPixelsOnHorizontal;
			BitmapPanel.Height = countOfPixelsOnVertical;
			this.Width = BitmapPanel.Left + BitmapPanel.Width + 20;
			this.Height = BitmapPanel.Top + BitmapPanel.Height + 35;

			PathTracing alg = new PathTracing(countOfShadowRays, absorption, countOfPixelsOnHorizontal, countOfPixelsOnVertical, room, light);
			alg.ComputeImage(countOfFrames);
			BitmapPanel.BackgroundImage = alg.Draw();

			SavePictureToFile();
		}

		private void SavePictureToFile()
		{
			string path = @"D:\\Projects\GlobalLighting\GlobalLighting\PathTracing\Statistics\";
			string nameOfFile = "CounterOfPictures";
			int counterOfPictures = 0;
			FileStream fileStream = null;

			if (!File.Exists(path + nameOfFile))
			{
				fileStream = File.Open(path + nameOfFile, FileMode.CreateNew);
				Process.Start("cmd.exe", @"/C D: && cd " + path + " && svn add " + nameOfFile);
			}
			else
			{
				//File.Decrypt(path + nameOfFile);
				fileStream = File.Open(path + nameOfFile, FileMode.Open);
			}

			using (StreamReader streamReader = new StreamReader(fileStream))
			{
				string line;
				if ((line = streamReader.ReadLine()) != null)
					counterOfPictures = Convert.ToInt32(line);
				streamReader.Close();
				using (StreamWriter streamWriter = new StreamWriter(fileStream.Name, false))
				{
					streamWriter.Write(++counterOfPictures);
					streamWriter.Close();
					//File.Encrypt(path + nameOfFile);
					Process.Start("cmd.exe", @"/C D: && cd " + path + " && svn add " + counterOfPictures + ".jpg");
				}
			}

			BitmapPanel.BackgroundImage.Save(path + counterOfPictures + ".jpg");
		}
	}
}