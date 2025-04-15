using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using HandheldCompanion.Inputs;
using HandheldCompanion.Managers;
using HandheldCompanion.Models;
using HandheldCompanion.Sensors;
using HandheldCompanion.Shared;
using static HandheldCompanion.Utils.DeviceUtils;

namespace HandheldCompanion.Devices;

public class IdeaPadSlim5 : IDevice
{
    public enum IdeaPadMode
    {
        Quiet = 0x01,
        Balanced = 0x02,
        Performance = 0x03,
        MaxPerformance = 0x04,
        Custom = 0xFF,
    }

    public enum CapabilityID
    {

    }

    public IdeaPadSlim5()
    {
        // device specific settings
        ProductIllustration = "device_legion_go";
        ProductModel = "IDEALPADSLIM5";

        // https://www.intel.com/content/www/us/en/products/sku/236847/intel-core-ultra-7-processor-155h-24m-cache-up-to-4-80-ghz/specifications.html
        nTDP = new double[] { 30, 30, 55 };
        cTDP = new double[] { 5, 65 };
        GfxClock = new double[] { 100, 2250 };
        CpuClock = 4800;

        // TODO
        // Quiet
        DevicePowerProfiles.Add(new(Properties.Resources.PowerProfileOneXPlayerX1IntelBetterBattery, Properties.Resources.PowerProfileOneXPlayerX1IntelBetterBatteryDesc)
        {
            Default = true,
            DeviceDefault = true,
            OSPowerMode = OSPowerMode.BetterBattery,
            CPUBoostLevel = CPUBoostLevel.Disabled,
            Guid = BetterBatteryGuid,
            TDPOverrideEnabled = true,
            TDPOverrideValues = new[] { 15.0d, 15.0d, 15.0d }
        });

        // Balanced
        DevicePowerProfiles.Add(new(Properties.Resources.PowerProfileOneXPlayerX1IntelBetterPerformance, Properties.Resources.PowerProfileOneXPlayerX1IntelBetterPerformanceDesc)
        {
            Default = true,
            DeviceDefault = true,
            OSPowerMode = OSPowerMode.BetterPerformance,
            CPUBoostLevel = CPUBoostLevel.Enabled,
            Guid = BetterPerformanceGuid,
            TDPOverrideEnabled = true,
            TDPOverrideValues = new[] { 30.0d, 30.0d, 30.0d }
        });

        // Performance
        DevicePowerProfiles.Add(new(Properties.Resources.PowerProfileOneXPlayerX1IntelBetterPerformance, Properties.Resources.PowerProfileOneXPlayerX1IntelBetterPerformanceDesc)
        {
            Default = true,
            DeviceDefault = true,
            OSPowerMode = OSPowerMode.BetterPerformance,
            CPUBoostLevel = CPUBoostLevel.Enabled,
            Guid = BetterPerformanceGuid,
            TDPOverrideEnabled = true,
            TDPOverrideValues = new[] { 40.0d, 40.0d, 55.0d }
        });

        // Max Performance
        DevicePowerProfiles.Add(new(Properties.Resources.PowerProfileOneXPlayerX1IntelBestPerformance, Properties.Resources.PowerProfileOneXPlayerX1IntelBestPerformanceDesc)
        {
            Default = true,
            DeviceDefault = true,
            OSPowerMode = OSPowerMode.BestPerformance,
            CPUBoostLevel = CPUBoostLevel.Enabled,
            Guid = BestPerformanceGuid,
            TDPOverrideEnabled = true,
            TDPOverrideValues = new[] { 45.0d, 45.0d, 64.0d },
            EPPOverrideEnabled = true,
            EPPOverrideValue = 64,
        });
    }

    public override bool Open()
    {
        var success = base.Open();
        if (!success)
            return false;

        return true;
    }

    // TODO Battery Protection (Conservation mode)
    private void QuerySettings()
    {
    }

    private void SettingsManager_Initialized()
    {
        QuerySettings();
    }

    public override void Close()
    {
        base.Close();
    }

    public override bool SetLedStatus(bool enable)
    {
        return true;
    }

    public override bool SetLedBrightness(int brightness)
    {
        return true;
    }

    public override bool SetLedColor(Color mainColor, Color secondaryColor, LEDLevel level, int speed = 100)
    {
        return true;
    }

    public override bool SetLEDPreset(LEDPreset? preset)
    {
        return true;
    }
}
