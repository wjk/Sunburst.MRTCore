// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#pragma once
#include "Microsoft.ApplicationModel.Resources.ResourceLoader.g.h"

namespace winrt::Microsoft::ApplicationModel::Resources::implementation
{

struct ResourceLoader : ResourceLoaderT<ResourceLoader>
{
    ResourceLoader();
    ResourceLoader(hstring const& fileName);
    ResourceLoader(hstring const& fileName, hstring const& resourceMap);
    ~ResourceLoader();

    static hstring GetDefaultResourceFilePath();

    hstring GetString(hstring const& resourceId);
    hstring GetStringForUri(Windows::Foundation::Uri const& resourceUri);

private:

    MrmManagerHandle m_resourceManager = nullptr;
    MrmMapHandle m_currentResourceMap = nullptr;
};

} // namespace winrt::Microsoft::ApplicationModel::Resources::implementation

namespace winrt::Microsoft::ApplicationModel::Resources::factory_implementation
{

struct ResourceLoader : ResourceLoaderT<ResourceLoader, implementation::ResourceLoader>
{};

} // namespace winrt::Microsoft::ApplicationModel::Resources::factory_implementation
