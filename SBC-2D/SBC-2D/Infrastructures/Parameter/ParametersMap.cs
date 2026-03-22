using Dapper.FluentMap;
using Dapper.FluentMap.Mapping;

namespace SBC_2D.Infrastructures.Parameter
{
    public class ParametersMap : EntityMap<Parameters>
    {
        public ParametersMap()
        {
            Map(p => p.Name).ToColumn("NAME");
            Map(p => p.IsMapModeBypass).ToColumn("IS_BYPASS_MAP_MODE");
            Map(p => p.IsUpperBrBypass).ToColumn("IS_BYPASS_UPPER_BR");
            Map(p => p.IsLowerBrBypass).ToColumn("IS_BYPASS_LOWER_BR");
            Map(p => p.IsLdsBypass).ToColumn("IS_BYPASS_LDS");
            Map(p => p.IsPcbRotate).ToColumn("IS_PCB_ROTATE");
            Map(p => p.ThicknessZeroBias).ToColumn("THICKNESS_ZERO_BIAS");
            Map(p => p.Thickness).ToColumn("THICKNESS");
            Map(p => p.ThicknessPosTolerance).ToColumn("THICKNESS_POS_TOLERANCE");
            Map(p => p.PcbCount).ToColumn("PCB_COUNT");
            Map(p => p.PcbCellsX).ToColumn("PCB_CELLS_X");
            Map(p => p.PcbCellsY).ToColumn("PCB_CELLS_Y");
            Map(p => p.PcbBlocksX).ToColumn("PCB_BLOCKS_X");
            Map(p => p.PcbBlocksY).ToColumn("PCB_BLOCKS_Y");
        }
    }
}
