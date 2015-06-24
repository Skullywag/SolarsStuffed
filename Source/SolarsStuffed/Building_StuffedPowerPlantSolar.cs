using System;
using UnityEngine;
using Verse;
using RimWorld;
namespace SolarsStuffed
{
    public class Building_StuffedPowerPlantSolar : Building_PowerPlant
    {
        private float FullSunPower;
        private const float NightPower = 0f;
        private static readonly Vector2 BarSize = new Vector2(2.3f, 0.14f);
        private static readonly Material BarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f));
        private static readonly Material BarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f));
        public override void SpawnSetup()
        {
            base.SpawnSetup();
            if (this.def.MadeFromStuff)
            {
                switch (this.Stuff.defName)
                {
                    case "Silver":
                        FullSunPower = 1900f;
                        break;
                    case "Gold":
                        FullSunPower = 2100f;
                        break;
                    case "Steel":
                        FullSunPower = 1700f;
                        break;
                    case "Plasteel":
                        FullSunPower = 2500f;
                        break;
                    case "Uranium":
                        FullSunPower = 2300f;
                        break;
                    default:
                        FullSunPower = 1700f;
                        break;
                }
            }
        }

        public override void Tick()
        {
            base.Tick();
            if (Find.RoofGrid.Roofed(base.Position))
            {
                this.powerComp.PowerOutput = 0f;
            }
            else
            {
                this.powerComp.PowerOutput = Mathf.Lerp(0f, FullSunPower, SkyManager.CurSkyGlow);
            }
        }
        public override void Draw()
        {
            base.Draw();
            GenDraw.FillableBarRequest r = default(GenDraw.FillableBarRequest);
            r.center = this.DrawPos + Vector3.up * 0.1f;
            r.size = Building_StuffedPowerPlantSolar.BarSize;
            r.fillPercent = this.powerComp.PowerOutput / FullSunPower;
            r.filledMat = Building_StuffedPowerPlantSolar.BarFilledMat;
            r.unfilledMat = Building_StuffedPowerPlantSolar.BarUnfilledMat;
            r.margin = 0.15f;
            Rot4 rotation = base.Rotation;
            rotation.Rotate(RotationDirection.Clockwise);
            r.rotation = rotation;
            GenDraw.DrawFillableBar(r);
        }
    }
}

