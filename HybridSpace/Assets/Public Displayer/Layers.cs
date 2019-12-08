// This file is dynamically created and overwritten. Changes should be made in LayersWriter.cs.
using System.Linq;

namespace UnityEngine
{
	public static class Layers
	{
		public const string Default = @"Default";
		public const string TransparentFX = @"TransparentFX";
		public const string IgnoreRaycast = @"Ignore Raycast";
		public const string Water = @"Water";
		public const string UI = @"UI";
		public const string a = @"a";
		
		/// <summary>
		/// Returns the shared layers.
		/// </summary>
		public static int Intersect(int layer1, int layer2, params int[] otherLayers) => otherLayers.Concat(new []{layer1, layer2}).Aggregate((current, next) => current & next);
		
		/// <summary>
		/// Returns all layers as one.
		/// </summary>
		public static int Combine(int layer1, int layer2, params int[] otherLayers) => otherLayers.Concat(new []{layer1, layer2}).Aggregate((current, next) => current | next);
		
		/// <summary>
		/// Returns the exclusive layers.
		/// </summary>
		public static int Difference(int layer1, int layer2) => layer1 ^ layer2;
		
		/// <summary>
		/// Returns all other layers.
		/// </summary>
		public static int Inverse(int layer) => ~layer;
		
		/// <summary>
		/// Returns the layers shift steps after this layer.
		/// </summary>
		public static int Next(int layer, int shift) => layer << shift;
		
		/// <summary>
		/// Returns the layers shift steps before this layer.
		/// </summary>
		public static int Previous(int layer, int shift) => layer >> shift;
	}
}
