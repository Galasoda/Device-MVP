using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBC_2D.Infrastructures.Parameter
{
    public class Parameters
    {
        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public DateTime UpdatedAt { get; set; }

        [Column("IS_BYPASS_MAP_MODE")]
        public bool IsMapModeBypass { get; set; }

        [Column("IS_BYPASS_UPPER_BR")]
        public bool IsUpperBrBypass { get; set; }

        [Column("IS_BYPASS_LOWER_BR")]
        public bool IsLowerBrBypass { get; set; }

        [Column("IS_BYPASS_LDS")]
        public bool IsLdsBypass { get; set; }

        [Column("IS_PCB_ROTATE")]
        public bool IsPcbRotate { get; set; }

        [Column("THICKNESS_ZERO_BIAS")]
        public int ThicknessZeroBias { get; set; }

        [Column("THICKNESS")]
        public int Thickness { get; set; }

        [Column("THICKNESS_POS_TOLERANCE")]
        public int ThicknessPosTolerance { get; set; }

        [Column("PCB_COUNT")]
        public int PcbCount { get; set; }

        [Column("PCB_CELLS_X")]
        public int PcbCellsX { get; set; }

        [Column("PCB_CELLS_Y")]
        public int PcbCellsY { get; set; }

        [Column("PCB_BLOCKS_X")]
        public int PcbBlocksX { get; set; }

        [Column("PCB_BLOCKS_Y")]
        public int PcbBlocksY { get; set; }

        [NotMapped]
        public int MaxThickness => Thickness + ThicknessPosTolerance;

        [NotMapped]
        public int PcbCells => PcbCellsX * PcbCellsY * PcbCount;
    }
}
