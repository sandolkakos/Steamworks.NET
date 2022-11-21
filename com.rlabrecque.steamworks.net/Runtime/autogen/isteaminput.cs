// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2022 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
	#define DISABLESTEAMWORKS
#endif

#if !DISABLESTEAMWORKS

using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace Steamworks {
	public static class SteamInput {
		/// <summary>
		/// <para> Init and Shutdown must be called when starting/ending use of this interface.</para>
		/// <para> if bExplicitlyCallRunFrame is called then you will need to manually call RunFrame</para>
		/// <para> each frame, otherwise Steam Input will updated when SteamAPI_RunCallbacks() is called</para>
		/// </summary>
		public static bool Init(bool bExplicitlyCallRunFrame) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_Init(CSteamAPIContext.GetSteamInput(), bExplicitlyCallRunFrame);
		}

		public static bool Shutdown() {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_Shutdown(CSteamAPIContext.GetSteamInput());
		}

		/// <summary>
		/// <para> Set the absolute path to the Input Action Manifest file containing the in-game actions</para>
		/// <para> and file paths to the official configurations. Used in games that bundle Steam Input</para>
		/// <para> configurations inside of the game depot instead of using the Steam Workshop</para>
		/// </summary>
		public static bool SetInputActionManifestFilePath(string pchInputActionManifestAbsolutePath) {
			InteropHelp.TestIfAvailableClient();
			using (var pchInputActionManifestAbsolutePath2 = new InteropHelp.UTF8StringHandle(pchInputActionManifestAbsolutePath)) {
				return NativeMethods.ISteamInput_SetInputActionManifestFilePath(CSteamAPIContext.GetSteamInput(), pchInputActionManifestAbsolutePath2);
			}
		}

		/// <summary>
		/// <para> Synchronize API state with the latest Steam Input action data available. This</para>
		/// <para> is performed automatically by SteamAPI_RunCallbacks, but for the absolute lowest</para>
		/// <para> possible latency, you call this directly before reading controller state.</para>
		/// <para> Note: This must be called from somewhere before GetConnectedControllers will</para>
		/// <para> return any handles</para>
		/// </summary>
		public static void RunFrame(bool bReservedValue = true) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_RunFrame(CSteamAPIContext.GetSteamInput(), bReservedValue);
		}

		/// <summary>
		/// <para> Waits on an IPC event from Steam sent when there is new data to be fetched from</para>
		/// <para> the data drop. Returns true when data was recievied before the timeout expires.</para>
		/// <para> Useful for games with a dedicated input thread</para>
		/// </summary>
		public static bool BWaitForData(bool bWaitForever, uint unTimeout) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_BWaitForData(CSteamAPIContext.GetSteamInput(), bWaitForever, unTimeout);
		}

		/// <summary>
		/// <para> Returns true if new data has been received since the last time action data was accessed</para>
		/// <para> via GetDigitalActionData or GetAnalogActionData. The game will still need to call</para>
		/// <para> SteamInput()-&gt;RunFrame() or SteamAPI_RunCallbacks() before this to update the data stream</para>
		/// </summary>
		public static bool BNewDataAvailable() {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_BNewDataAvailable(CSteamAPIContext.GetSteamInput());
		}

		/// <summary>
		/// <para> Enumerate currently connected Steam Input enabled devices - developers can opt in controller by type (ex: Xbox/Playstation/etc) via</para>
		/// <para> the Steam Input settings in the Steamworks site or users can opt-in in their controller settings in Steam.</para>
		/// <para> handlesOut should point to a STEAM_INPUT_MAX_COUNT sized array of InputHandle_t handles</para>
		/// <para> Returns the number of handles written to handlesOut</para>
		/// </summary>
		public static int GetConnectedControllers(InputHandle_t[] handlesOut) {
			InteropHelp.TestIfAvailableClient();
			if (handlesOut != null && handlesOut.Length != Constants.STEAM_INPUT_MAX_COUNT) {
				throw new System.ArgumentException("handlesOut must be the same size as Constants.STEAM_INPUT_MAX_COUNT!");
			}
			return NativeMethods.ISteamInput_GetConnectedControllers(CSteamAPIContext.GetSteamInput(), handlesOut);
		}

		/// <summary>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> CALLBACKS</para>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> Controller configuration loaded - these callbacks will always fire if you have</para>
		/// <para> a handler. Note: this is called within either SteamInput()-&gt;RunFrame or by SteamAPI_RunCallbacks</para>
		/// <para> Enable SteamInputDeviceConnected_t and SteamInputDeviceDisconnected_t callbacks.</para>
		/// <para> Each controller that is already connected will generate a device connected</para>
		/// <para> callback when you enable them</para>
		/// </summary>
		public static void EnableDeviceCallbacks() {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_EnableDeviceCallbacks(CSteamAPIContext.GetSteamInput());
		}

		/// <summary>
		/// <para> Controller Connected - provides info about a single newly connected controller</para>
		/// <para> Note: this is called within either SteamInput()-&gt;RunFrame or by SteamAPI_RunCallbacks</para>
		/// <para> Controller Disconnected - provides info about a single disconnected controller</para>
		/// <para> Note: this is called within either SteamInput()-&gt;RunFrame or by SteamAPI_RunCallbacks</para>
		/// <para> Enable SteamInputActionEvent_t callbacks. Directly calls your callback function</para>
		/// <para> for lower latency than standard Steam callbacks. Supports one callback at a time.</para>
		/// <para> Note: this is called within either SteamInput()-&gt;RunFrame or by SteamAPI_RunCallbacks</para>
		/// </summary>
		public static void EnableActionEventCallbacks(SteamInputActionEventCallbackPointer pCallback) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_EnableActionEventCallbacks(CSteamAPIContext.GetSteamInput(), pCallback);
		}

		/// <summary>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> ACTION SETS</para>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> Lookup the handle for an Action Set. Best to do this once on startup, and store the handles for all future API calls.</para>
		/// </summary>
		public static InputActionSetHandle_t GetActionSetHandle(string pszActionSetName) {
			InteropHelp.TestIfAvailableClient();
			using (var pszActionSetName2 = new InteropHelp.UTF8StringHandle(pszActionSetName)) {
				return (InputActionSetHandle_t)NativeMethods.ISteamInput_GetActionSetHandle(CSteamAPIContext.GetSteamInput(), pszActionSetName2);
			}
		}

		/// <summary>
		/// <para> Reconfigure the controller to use the specified action set (ie 'Menu', 'Walk' or 'Drive')</para>
		/// <para> This is cheap, and can be safely called repeatedly. It's often easier to repeatedly call it in</para>
		/// <para> your state loops, instead of trying to place it in all of your state transitions.</para>
		/// </summary>
		public static void ActivateActionSet(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_ActivateActionSet(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle);
		}

		public static InputActionSetHandle_t GetCurrentActionSet(InputHandle_t inputHandle) {
			InteropHelp.TestIfAvailableClient();
			return (InputActionSetHandle_t)NativeMethods.ISteamInput_GetCurrentActionSet(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		/// <summary>
		/// <para> ACTION SET LAYERS</para>
		/// </summary>
		public static void ActivateActionSetLayer(InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_ActivateActionSetLayer(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetLayerHandle);
		}

		public static void DeactivateActionSetLayer(InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_DeactivateActionSetLayer(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetLayerHandle);
		}

		public static void DeactivateAllActionSetLayers(InputHandle_t inputHandle) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_DeactivateAllActionSetLayers(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		/// <summary>
		/// <para> Enumerate currently active layers.</para>
		/// <para> handlesOut should point to a STEAM_INPUT_MAX_ACTIVE_LAYERS sized array of InputActionSetHandle_t handles</para>
		/// <para> Returns the number of handles written to handlesOut</para>
		/// </summary>
		public static int GetActiveActionSetLayers(InputHandle_t inputHandle, InputActionSetHandle_t[] handlesOut) {
			InteropHelp.TestIfAvailableClient();
			if (handlesOut != null && handlesOut.Length != Constants.STEAM_INPUT_MAX_ACTIVE_LAYERS) {
				throw new System.ArgumentException("handlesOut must be the same size as Constants.STEAM_INPUT_MAX_ACTIVE_LAYERS!");
			}
			return NativeMethods.ISteamInput_GetActiveActionSetLayers(CSteamAPIContext.GetSteamInput(), inputHandle, handlesOut);
		}

		/// <summary>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> ACTIONS</para>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> Lookup the handle for a digital action. Best to do this once on startup, and store the handles for all future API calls.</para>
		/// </summary>
		public static InputDigitalActionHandle_t GetDigitalActionHandle(string pszActionName) {
			InteropHelp.TestIfAvailableClient();
			using (var pszActionName2 = new InteropHelp.UTF8StringHandle(pszActionName)) {
				return (InputDigitalActionHandle_t)NativeMethods.ISteamInput_GetDigitalActionHandle(CSteamAPIContext.GetSteamInput(), pszActionName2);
			}
		}

		/// <summary>
		/// <para> Returns the current state of the supplied digital game action</para>
		/// </summary>
		public static InputDigitalActionData_t GetDigitalActionData(InputHandle_t inputHandle, InputDigitalActionHandle_t digitalActionHandle) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetDigitalActionData(CSteamAPIContext.GetSteamInput(), inputHandle, digitalActionHandle);
		}

		/// <summary>
		/// <para> Get the origin(s) for a digital action within an action set. Returns the number of origins supplied in originsOut. Use this to display the appropriate on-screen prompt for the action.</para>
		/// <para> originsOut should point to a STEAM_INPUT_MAX_ORIGINS sized array of EInputActionOrigin handles. The EInputActionOrigin enum will get extended as support for new controller controllers gets added to</para>
		/// <para> the Steam client and will exceed the values from this header, please check bounds if you are using a look up table.</para>
		/// </summary>
		public static int GetDigitalActionOrigins(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle, EInputActionOrigin[] originsOut) {
			InteropHelp.TestIfAvailableClient();
			if (originsOut != null && originsOut.Length != Constants.STEAM_INPUT_MAX_ORIGINS) {
				throw new System.ArgumentException("originsOut must be the same size as Constants.STEAM_INPUT_MAX_ORIGINS!");
			}
			return NativeMethods.ISteamInput_GetDigitalActionOrigins(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle, digitalActionHandle, originsOut);
		}

		/// <summary>
		/// <para> Returns a localized string (from Steam's language setting) for the user-facing action name corresponding to the specified handle</para>
		/// </summary>
		public static string GetStringForDigitalActionName(InputDigitalActionHandle_t eActionHandle) {
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetStringForDigitalActionName(CSteamAPIContext.GetSteamInput(), eActionHandle));
		}

		/// <summary>
		/// <para> Lookup the handle for an analog action. Best to do this once on startup, and store the handles for all future API calls.</para>
		/// </summary>
		public static InputAnalogActionHandle_t GetAnalogActionHandle(string pszActionName) {
			InteropHelp.TestIfAvailableClient();
			using (var pszActionName2 = new InteropHelp.UTF8StringHandle(pszActionName)) {
				return (InputAnalogActionHandle_t)NativeMethods.ISteamInput_GetAnalogActionHandle(CSteamAPIContext.GetSteamInput(), pszActionName2);
			}
		}

		/// <summary>
		/// <para> Returns the current state of these supplied analog game action</para>
		/// </summary>
		public static InputAnalogActionData_t GetAnalogActionData(InputHandle_t inputHandle, InputAnalogActionHandle_t analogActionHandle) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetAnalogActionData(CSteamAPIContext.GetSteamInput(), inputHandle, analogActionHandle);
		}

		/// <summary>
		/// <para> Get the origin(s) for an analog action within an action set. Returns the number of origins supplied in originsOut. Use this to display the appropriate on-screen prompt for the action.</para>
		/// <para> originsOut should point to a STEAM_INPUT_MAX_ORIGINS sized array of EInputActionOrigin handles. The EInputActionOrigin enum will get extended as support for new controller controllers gets added to</para>
		/// <para> the Steam client and will exceed the values from this header, please check bounds if you are using a look up table.</para>
		/// </summary>
		public static int GetAnalogActionOrigins(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputAnalogActionHandle_t analogActionHandle, EInputActionOrigin[] originsOut) {
			InteropHelp.TestIfAvailableClient();
			if (originsOut != null && originsOut.Length != Constants.STEAM_INPUT_MAX_ORIGINS) {
				throw new System.ArgumentException("originsOut must be the same size as Constants.STEAM_INPUT_MAX_ORIGINS!");
			}
			return NativeMethods.ISteamInput_GetAnalogActionOrigins(CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle, analogActionHandle, originsOut);
		}

		/// <summary>
		/// <para> Get a local path to a PNG file for the provided origin's glyph.</para>
		/// </summary>
		public static string GetGlyphPNGForActionOrigin(EInputActionOrigin eOrigin, ESteamInputGlyphSize eSize, uint unFlags) {
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetGlyphPNGForActionOrigin(CSteamAPIContext.GetSteamInput(), eOrigin, eSize, unFlags));
		}

		/// <summary>
		/// <para> Get a local path to a SVG file for the provided origin's glyph.</para>
		/// </summary>
		public static string GetGlyphSVGForActionOrigin(EInputActionOrigin eOrigin, uint unFlags) {
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetGlyphSVGForActionOrigin(CSteamAPIContext.GetSteamInput(), eOrigin, unFlags));
		}

		/// <summary>
		/// <para> Get a local path to an older, Big Picture Mode-style PNG file for a particular origin</para>
		/// </summary>
		public static string GetGlyphForActionOrigin_Legacy(EInputActionOrigin eOrigin) {
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetGlyphForActionOrigin_Legacy(CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		/// <summary>
		/// <para> Returns a localized string (from Steam's language setting) for the specified origin.</para>
		/// </summary>
		public static string GetStringForActionOrigin(EInputActionOrigin eOrigin) {
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetStringForActionOrigin(CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		/// <summary>
		/// <para> Returns a localized string (from Steam's language setting) for the user-facing action name corresponding to the specified handle</para>
		/// </summary>
		public static string GetStringForAnalogActionName(InputAnalogActionHandle_t eActionHandle) {
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetStringForAnalogActionName(CSteamAPIContext.GetSteamInput(), eActionHandle));
		}

		/// <summary>
		/// <para> Stop analog momentum for the action if it is a mouse action in trackball mode</para>
		/// </summary>
		public static void StopAnalogActionMomentum(InputHandle_t inputHandle, InputAnalogActionHandle_t eAction) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_StopAnalogActionMomentum(CSteamAPIContext.GetSteamInput(), inputHandle, eAction);
		}

		/// <summary>
		/// <para> Returns raw motion data from the specified device</para>
		/// </summary>
		public static InputMotionData_t GetMotionData(InputHandle_t inputHandle) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetMotionData(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		/// <summary>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> OUTPUTS</para>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> Trigger a vibration event on supported controllers - Steam will translate these commands into haptic pulses for Steam Controllers</para>
		/// </summary>
		public static void TriggerVibration(InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_TriggerVibration(CSteamAPIContext.GetSteamInput(), inputHandle, usLeftSpeed, usRightSpeed);
		}

		/// <summary>
		/// <para> Trigger a vibration event on supported controllers including Xbox trigger impulse rumble - Steam will translate these commands into haptic pulses for Steam Controllers</para>
		/// </summary>
		public static void TriggerVibrationExtended(InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed, ushort usLeftTriggerSpeed, ushort usRightTriggerSpeed) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_TriggerVibrationExtended(CSteamAPIContext.GetSteamInput(), inputHandle, usLeftSpeed, usRightSpeed, usLeftTriggerSpeed, usRightTriggerSpeed);
		}

		/// <summary>
		/// <para> Send a haptic pulse, works on Steam Deck and Steam Controller devices</para>
		/// </summary>
		public static void TriggerSimpleHapticEvent(InputHandle_t inputHandle, EControllerHapticLocation eHapticLocation, byte nIntensity, char nGainDB, byte nOtherIntensity, char nOtherGainDB) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_TriggerSimpleHapticEvent(CSteamAPIContext.GetSteamInput(), inputHandle, eHapticLocation, nIntensity, nGainDB, nOtherIntensity, nOtherGainDB);
		}

		/// <summary>
		/// <para> Set the controller LED color on supported controllers. nFlags is a bitmask of values from ESteamInputLEDFlag - 0 will default to setting a color. Steam will handle</para>
		/// <para> the behavior on exit of your program so you don't need to try restore the default as you are shutting down</para>
		/// </summary>
		public static void SetLEDColor(InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_SetLEDColor(CSteamAPIContext.GetSteamInput(), inputHandle, nColorR, nColorG, nColorB, nFlags);
		}

		/// <summary>
		/// <para> Trigger a haptic pulse on a Steam Controller - if you are approximating rumble you may want to use TriggerVibration instead.</para>
		/// <para> Good uses for Haptic pulses include chimes, noises, or directional gameplay feedback (taking damage, footstep locations, etc).</para>
		/// </summary>
		public static void Legacy_TriggerHapticPulse(InputHandle_t inputHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_Legacy_TriggerHapticPulse(CSteamAPIContext.GetSteamInput(), inputHandle, eTargetPad, usDurationMicroSec);
		}

		/// <summary>
		/// <para> Trigger a haptic pulse with a duty cycle of usDurationMicroSec / usOffMicroSec, unRepeat times. If you are approximating rumble you may want to use TriggerVibration instead.</para>
		/// <para> nFlags is currently unused and reserved for future use.</para>
		/// </summary>
		public static void Legacy_TriggerRepeatedHapticPulse(InputHandle_t inputHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_Legacy_TriggerRepeatedHapticPulse(CSteamAPIContext.GetSteamInput(), inputHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
		}

		/// <summary>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> Utility functions available without using the rest of Steam Input API</para>
		/// <para>-----------------------------------------------------------------------------</para>
		/// <para> Invokes the Steam overlay and brings up the binding screen if the user is using Big Picture Mode</para>
		/// <para> If the user is not in Big Picture Mode it will open up the binding in a new window</para>
		/// </summary>
		public static bool ShowBindingPanel(InputHandle_t inputHandle) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_ShowBindingPanel(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		/// <summary>
		/// <para> Returns the input type for a particular handle - unlike EInputActionOrigin which update with Steam and may return unrecognized values</para>
		/// <para> ESteamInputType will remain static and only return valid values from your SDK version</para>
		/// </summary>
		public static ESteamInputType GetInputTypeForHandle(InputHandle_t inputHandle) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetInputTypeForHandle(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		/// <summary>
		/// <para> Returns the associated controller handle for the specified emulated gamepad - can be used with the above 2 functions</para>
		/// <para> to identify controllers presented to your game over Xinput. Returns 0 if the Xinput index isn't associated with Steam Input</para>
		/// </summary>
		public static InputHandle_t GetControllerForGamepadIndex(int nIndex) {
			InteropHelp.TestIfAvailableClient();
			return (InputHandle_t)NativeMethods.ISteamInput_GetControllerForGamepadIndex(CSteamAPIContext.GetSteamInput(), nIndex);
		}

		/// <summary>
		/// <para> Returns the associated gamepad index for the specified controller, if emulating a gamepad or -1 if not associated with an Xinput index</para>
		/// </summary>
		public static int GetGamepadIndexForController(InputHandle_t ulinputHandle) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetGamepadIndexForController(CSteamAPIContext.GetSteamInput(), ulinputHandle);
		}

		/// <summary>
		/// <para> Returns a localized string (from Steam's language setting) for the specified Xbox controller origin.</para>
		/// </summary>
		public static string GetStringForXboxOrigin(EXboxOrigin eOrigin) {
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetStringForXboxOrigin(CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		/// <summary>
		/// <para> Get a local path to art for on-screen glyph for a particular Xbox controller origin</para>
		/// </summary>
		public static string GetGlyphForXboxOrigin(EXboxOrigin eOrigin) {
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamInput_GetGlyphForXboxOrigin(CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		/// <summary>
		/// <para> Get the equivalent ActionOrigin for a given Xbox controller origin this can be chained with GetGlyphForActionOrigin to provide future proof glyphs for</para>
		/// <para> non-Steam Input API action games. Note - this only translates the buttons directly and doesn't take into account any remapping a user has made in their configuration</para>
		/// </summary>
		public static EInputActionOrigin GetActionOriginFromXboxOrigin(InputHandle_t inputHandle, EXboxOrigin eOrigin) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetActionOriginFromXboxOrigin(CSteamAPIContext.GetSteamInput(), inputHandle, eOrigin);
		}

		/// <summary>
		/// <para> Convert an origin to another controller type - for inputs not present on the other controller type this will return k_EInputActionOrigin_None</para>
		/// <para> When a new input type is added you will be able to pass in k_ESteamInputType_Unknown and the closest origin that your version of the SDK recognized will be returned</para>
		/// <para> ex: if a Playstation 5 controller was released this function would return Playstation 4 origins.</para>
		/// </summary>
		public static EInputActionOrigin TranslateActionOrigin(ESteamInputType eDestinationInputType, EInputActionOrigin eSourceOrigin) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_TranslateActionOrigin(CSteamAPIContext.GetSteamInput(), eDestinationInputType, eSourceOrigin);
		}

		/// <summary>
		/// <para> Get the binding revision for a given device. Returns false if the handle was not valid or if a mapping is not yet loaded for the device</para>
		/// </summary>
		public static bool GetDeviceBindingRevision(InputHandle_t inputHandle, out int pMajor, out int pMinor) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetDeviceBindingRevision(CSteamAPIContext.GetSteamInput(), inputHandle, out pMajor, out pMinor);
		}

		/// <summary>
		/// <para> Get the Steam Remote Play session ID associated with a device, or 0 if there is no session associated with it</para>
		/// <para> See isteamremoteplay.h for more information on Steam Remote Play sessions</para>
		/// </summary>
		public static uint GetRemotePlaySessionID(InputHandle_t inputHandle) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetRemotePlaySessionID(CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		/// <summary>
		/// <para> Get a bitmask of the Steam Input Configuration types opted in for the current session. Returns ESteamInputConfigurationEnableType values.?</para>
		/// <para> Note: user can override the settings from the Steamworks Partner site so the returned values may not exactly match your default configuration</para>
		/// </summary>
		public static ushort GetSessionInputConfigurationSettings() {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInput_GetSessionInputConfigurationSettings(CSteamAPIContext.GetSteamInput());
		}

		/// <summary>
		/// <para> Set the trigger effect for a DualSense controller</para>
		/// </summary>
		public static void SetDualSenseTriggerEffect(InputHandle_t inputHandle, IntPtr pParam) {
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInput_SetDualSenseTriggerEffect(CSteamAPIContext.GetSteamInput(), inputHandle, pParam);
		}
	}
}

#endif // !DISABLESTEAMWORKS
