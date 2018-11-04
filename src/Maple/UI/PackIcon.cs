﻿using System.Collections.Generic;
using System.Windows;

using MahApps.Metro.IconPacks;

using Maple.Domain;

namespace Maple
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="MahApps.Metro.IconPacks.PackIcon{Maple.PackIconKind}" />
    public class PackIcon : PackIcon<PackIconKind>
    {
        private static IDictionary<PackIconKind, string> _cache;

        /// <summary>
        /// Initializes the <see cref="PackIcon"/> class.
        /// </summary>
        static PackIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIcon), new FrameworkPropertyMetadata(typeof(PackIcon)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackIcon"/> class.
        /// </summary>
        public PackIcon() : base(CreateIconData)
        {
        }

        /// <summary>
        /// Tries the get.
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static bool TryGet(PackIconKind kind, out string data)
        {
            if (_cache == null)
                SeedCache();

            if (_cache.ContainsKey(kind))
            {
                data = _cache[kind];
                return true;
            }

            data = string.Empty;
            return false;
        }

        /// <summary>
        /// Creates the icon data.
        /// </summary>
        /// <returns></returns>
        private static IDictionary<PackIconKind, string> CreateIconData()
        {
            if (_cache == null)
                SeedCache();

            return _cache;
        }

        private static void SeedCache()
        {
            _cache = new Dictionary<PackIconKind, string>
        {
            {PackIconKind.ApplicationIcon, @"M 317.41,119.00
           C 317.41,119.00 321.00,141.00 321.00,141.00
             321.00,141.00 337.00,136.00 337.00,136.00
             337.00,136.00 332.35,161.00 332.35,161.00
             332.35,161.00 330.00,175.00 330.00,175.00
             335.31,173.69 344.77,171.94 350.00,173.00
             346.26,188.98 332.88,203.14 321.00,213.83
             316.91,217.51 308.80,223.39 305.63,227.28
             302.68,230.92 297.99,239.40 303.23,242.95
             304.70,243.95 306.27,244.22 308.00,244.00
             316.38,242.93 332.68,226.03 340.00,220.09
             344.71,216.28 349.67,211.88 356.00,213.00
             356.00,213.00 354.00,225.00 354.00,225.00
             354.00,225.00 355.00,226.00 355.00,226.00
             365.51,222.96 381.06,221.01 392.00,221.00
             392.00,221.00 387.00,232.00 387.00,232.00
             387.00,232.00 388.00,233.00 388.00,233.00
             388.00,233.00 424.00,234.00 424.00,234.00
             421.85,241.31 414.58,245.71 409.00,250.42
             404.53,254.20 400.34,258.52 395.00,261.00
             395.00,261.00 395.00,263.00 395.00,263.00
             395.00,263.00 407.00,268.00 407.00,268.00
             407.00,268.00 407.00,270.00 407.00,270.00
             395.90,275.45 382.92,282.78 371.00,286.00
             373.23,287.92 381.86,294.04 382.51,296.13
             384.15,301.42 371.06,301.96 368.00,302.00
             368.00,302.00 334.72,297.84 334.72,297.84
             330.49,297.97 320.42,299.29 320.34,305.01
             320.29,308.66 326.33,312.50 329.00,314.42
             336.53,319.83 351.72,328.67 357.00,335.00
             338.25,340.48 323.31,342.63 304.00,337.36
             304.00,337.36 286.00,330.00 286.00,330.00
             286.00,330.00 292.00,343.00 292.00,343.00
             279.36,340.12 266.32,331.86 259.10,321.00
             254.90,314.67 254.54,310.28 251.69,304.00
             251.69,304.00 246.00,293.00 246.00,293.00
             246.00,293.00 258.00,297.00 258.00,297.00
             254.05,295.28 252.24,294.85 250.00,291.00
             250.00,291.00 282.00,283.00 282.00,283.00
             271.50,283.95 262.23,288.99 245.00,289.00
             245.00,289.00 252.00,264.00 252.00,264.00
             252.00,264.00 243.00,288.00 243.00,288.00
             243.00,288.00 232.00,278.00 232.00,278.00
             232.00,278.00 237.00,287.00 237.00,287.00
             237.00,287.00 229.00,287.00 229.00,287.00
             229.00,287.00 237.00,290.00 237.00,290.00
             237.00,290.00 220.00,299.52 220.00,299.52
             210.24,303.58 201.21,303.41 191.00,301.87
             185.06,300.97 177.22,299.51 174.00,294.00
             174.00,294.00 187.00,292.00 187.00,292.00
             187.00,292.00 187.00,290.00 187.00,290.00
             187.00,290.00 169.00,283.88 169.00,283.88
             154.51,277.18 146.22,268.17 137.69,255.00
             135.12,251.02 133.24,248.63 132.00,244.00
             132.00,244.00 150.00,246.58 150.00,246.58
             150.00,246.58 162.00,248.58 162.00,248.58
             164.52,249.03 169.65,250.16 171.96,249.36
             180.57,246.38 174.34,237.20 170.68,233.04
             160.96,222.02 140.23,218.67 133.00,198.00
             133.00,198.00 151.00,198.00 151.00,198.00
             146.32,190.61 137.33,169.50 136.00,161.00
             136.00,161.00 150.00,164.00 150.00,164.00
             147.99,152.95 146.00,136.14 146.00,125.00
             146.00,125.00 177.00,149.00 177.00,149.00
             177.00,149.00 179.00,138.00 179.00,138.00
             190.89,143.62 197.45,161.56 206.00,166.00
             206.99,163.67 210.26,156.59 213.67,157.71
             216.04,158.49 217.12,164.78 217.54,167.00
             217.54,167.00 222.04,204.00 222.04,204.00
             223.28,208.22 226.91,216.99 232.79,213.21
             240.56,208.21 238.02,195.30 236.46,188.00
             233.36,174.02 232.71,154.85 236.46,141.00
             236.46,141.00 241.00,129.00 241.00,129.00
             246.34,132.34 253.11,141.38 256.00,147.00
             256.00,147.00 258.00,147.00 258.00,147.00
             258.00,147.00 277.00,113.00 277.00,113.00
             277.00,113.00 279.00,113.00 279.00,113.00
             279.00,113.00 286.00,127.00 286.00,127.00
             286.00,127.00 314.00,106.00 314.00,106.00
             314.00,106.00 317.41,119.00 317.41,119.00 Z
           M 268.00,225.00
           C 268.00,225.00 269.00,226.00 269.00,226.00
             269.00,226.00 269.00,225.00 269.00,225.00
             269.00,225.00 268.00,225.00 268.00,225.00 Z
           M 267.00,228.00
           C 267.00,228.00 268.00,229.00 268.00,229.00
             268.00,229.00 268.00,228.00 268.00,228.00
             268.00,228.00 267.00,228.00 267.00,228.00 Z
           M 265.00,232.00
           C 265.00,232.00 266.00,233.00 266.00,233.00
             266.00,233.00 266.00,232.00 266.00,232.00
             266.00,232.00 265.00,232.00 265.00,232.00 Z
           M 206.00,236.00
           C 206.00,236.00 207.00,237.00 207.00,237.00
             207.00,237.00 207.00,236.00 207.00,236.00
             207.00,236.00 206.00,236.00 206.00,236.00 Z
           M 207.00,238.00
           C 207.00,238.00 208.00,239.00 208.00,239.00
             208.00,239.00 208.00,238.00 208.00,238.00
             208.00,238.00 207.00,238.00 207.00,238.00 Z
           M 208.00,240.00
           C 208.00,240.00 209.00,241.00 209.00,241.00
             209.00,241.00 209.00,240.00 209.00,240.00
             209.00,240.00 208.00,240.00 208.00,240.00 Z
           M 262.00,240.00
           C 262.00,240.00 263.00,241.00 263.00,241.00
             263.00,241.00 263.00,240.00 263.00,240.00
             263.00,240.00 262.00,240.00 262.00,240.00 Z
           M 210.00,243.00
           C 210.00,243.00 211.00,244.00 211.00,244.00
             211.00,244.00 211.00,243.00 211.00,243.00
             211.00,243.00 210.00,243.00 210.00,243.00 Z
           M 211.00,245.00
           C 211.00,245.00 212.00,246.00 212.00,246.00
             212.00,246.00 212.00,245.00 212.00,245.00
             212.00,245.00 211.00,245.00 211.00,245.00 Z
           M 219.00,258.00
           C 219.00,258.00 217.00,253.00 217.00,253.00
             217.00,253.00 219.00,258.00 219.00,258.00 Z
           M 254.00,264.00
           C 254.00,264.00 255.00,259.00 255.00,259.00
             255.00,259.00 254.00,264.00 254.00,264.00 Z
           M 226.00,269.00
           C 226.00,269.00 223.00,263.00 223.00,263.00
             222.66,266.34 223.61,266.83 226.00,269.00 Z
           M 230.00,275.00
           C 230.00,275.00 227.00,269.00 227.00,269.00
             226.66,272.34 227.61,272.83 230.00,275.00 Z
           M 303.00,276.00
           C 303.00,276.00 304.00,277.00 304.00,277.00
             304.00,277.00 304.00,276.00 304.00,276.00
             304.00,276.00 303.00,276.00 303.00,276.00 Z
           M 299.00,277.00
           C 299.00,277.00 300.00,278.00 300.00,278.00
             300.00,278.00 300.00,277.00 300.00,277.00
             300.00,277.00 299.00,277.00 299.00,277.00 Z
           M 218.00,282.00
           C 218.00,282.00 219.00,283.00 219.00,283.00
             219.00,283.00 219.00,282.00 219.00,282.00
             219.00,282.00 218.00,282.00 218.00,282.00 Z
           M 222.00,285.00
           C 222.00,285.00 229.00,285.00 229.00,285.00
             229.00,285.00 222.00,285.00 222.00,285.00 Z
           M 198.83,407.26
           C 197.71,405.63 198.30,402.85 198.83,401.00
             198.83,401.00 202.86,385.00 202.86,385.00
             209.61,361.28 219.27,338.77 228.55,316.00
             228.55,316.00 236.74,297.00 236.74,297.00
             238.83,292.76 238.91,290.33 244.00,291.00
             244.00,291.00 235.20,313.00 235.20,313.00
             235.20,313.00 215.12,369.00 215.12,369.00
             211.10,381.95 207.12,395.33 207.00,409.00
             204.70,408.98 200.57,409.26 198.83,407.26 Z
           M 262.00,301.00
           C 262.00,301.00 269.00,302.00 269.00,302.00
             269.00,302.00 262.00,301.00 262.00,301.00 Z
           M 271.00,305.00
           C 271.00,305.00 276.00,305.00 276.00,305.00
             276.00,305.00 272.00,304.00 272.00,304.00
             272.00,304.00 271.00,305.00 271.00,305.00 Z"},
        };
        }
    }
}
