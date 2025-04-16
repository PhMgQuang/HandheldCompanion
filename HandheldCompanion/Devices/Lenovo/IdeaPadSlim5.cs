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
        cTDP = new double[] { 10, 65 };
        GfxClock = new double[] { 100, 2250 };
        CpuClock = 4800;

        DevicePowerProfiles.Clear();

        // default profile
        DevicePowerProfiles.Add(new(Properties.Resources.PowerProfileDefaultName, Properties.Resources.PowerProfileDefaultDescription)
        {
            Default = true,
            Guid = Guid.Empty,
            OSPowerMode = OSPowerMode.BetterBattery,
            TDPOverrideValues = new double[] { this.nTDP[0], this.nTDP[1], this.nTDP[2] }
        });

        // TODO
        // Quiet
        DevicePowerProfiles.Add(new(Properties.Resources.PowerProfileLegionGoBetterBattery, Properties.Resources.PowerProfileLegionGoBetterBatteryDesc)
        {
            Default = true,
            DeviceDefault = true,
            OSPowerMode = OSPowerMode.BetterBattery,
            CPUBoostLevel = CPUBoostLevel.Disabled,
            Guid = BetterBatteryGuid,
            TDPOverrideEnabled = true,
            TDPOverrideValues = new[] { 15.0d, 15.0d, 30.0d }
        });

        // Balanced
        DevicePowerProfiles.Add(new(Properties.Resources.PowerProfileLegionGoBetterPerformance, Properties.Resources.PowerProfileLegionGoBetterPerformanceDesc)
        {
            Default = true,
            DeviceDefault = true,
            OSPowerMode = OSPowerMode.BetterPerformance,
            CPUBoostLevel = CPUBoostLevel.Enabled,
            Guid = BetterPerformanceGuid,
            TDPOverrideEnabled = true,
            TDPOverrideValues = new[] { 30.0d, 30.0d, 55.0d }
        });

        // Performance
        DevicePowerProfiles.Add(new(Properties.Resources.PowerProfileLegionGoBestPerformance, Properties.Resources.PowerProfileLegionGoBestPerformanceDesc)
        {
            Default = true,
            DeviceDefault = true,
            OSPowerMode = OSPowerMode.BetterPerformance,
            CPUBoostLevel = CPUBoostLevel.Enabled,
            Guid = BestPerformanceGuid,
            TDPOverrideEnabled = true,
            TDPOverrideValues = new[] { 40.0d, 40.0d, 60.0d }
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
