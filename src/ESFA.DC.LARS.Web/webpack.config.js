const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const postcssPresetEnv = require('postcss-preset-env');
const VueLoaderPlugin = require('vue-loader/lib/plugin');

// We are getting 'process.env.NODE_ENV' from the NPM scripts
// Remember the 'dev' script?
const devMode = process.env.NODE_ENV !== 'production';
module.exports = {
    // Tells Webpack which built-in optimizations to use
    // If you leave this out, Webpack will default to 'production'
    mode: devMode ? 'development' : 'production',
    // Webpack needs to know where to start the bundling process,
    // so we define the Sass file under '/scss' directory
    // and the script file under '/js' directory


    entry: {
        './assets/dist/js/site': './wwwroot/assets/js/site.js',
        './assets/dist/js/pollyfills' : ['./wwwroot/pollyfills.js'],
        site : ['./wwwroot/assets/scss/site.scss'],
        './assets/dist/js/search': './wwwroot/app/searchMain.ts',
        './assets/dist/js/learningAimSearchResults': './wwwroot/app/learningAimSearchResultsMain.ts',
        './assets/dist/js/frameworkSearchResults': './wwwroot/app/frameworkSearchResultsMain.ts',
        './assets/dist/js/unitsSearchResults': './wwwroot/app/unitsSearchResultsMain.ts',
        './assets/dist/js/standardSearchResults': './wwwroot/app/standardSearchResultsMain.ts',
        './assets/dist/js/tLevelSearchResults': './wwwroot/app/tLevelSearchResultsMain.ts',
        './assets/dist/js/ESFA': './wwwroot/ESFAGlobals.ts'
    },
    // This is where we define the path where Webpack will place
    // a bundled JS file.
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, 'wwwroot')
    },
    devtool: devMode ? 'inline-source-map' : 'source-map',
    module: {
        rules: [
            {
                test: /\.ts$/,
                loader: 'ts-loader',
                exclude: /node_modules/,
                options: {
                    appendTsSuffixTo: [/\.vue$/]
                }
            },
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    loaders: {
                        'scss': 'vue-style-loader!css-loader!sass-loader',
                        'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax'
                    }
                }
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            },
            {
                // Look for Sass files and process them according to the
                // rules specified in the different loaders
                test: /\.(sa|sc)ss$/,
                // Use the following loaders from right-to-left, so it will
                // use sass-loader first and ending with MiniCssExtractPlugin
                use: [
                    {
                        // Extracts the CSS into a separate file and uses the
                        // defined configurations in the 'plugins' section
                        loader: MiniCssExtractPlugin.loader
                    },
                    {
                        // Interprets CSS
                        loader: 'css-loader',
                        options: {
                            importLoaders: 2
                        }
                    },
                    {
                        // Use PostCSS to minify and autoprefix with vendor rules
                        // for older browser compatibility
                        loader: 'postcss-loader',
                        options: {
                            ident: 'postcss',
                            // We instruct PostCSS to autoprefix and minimize our
                            // CSS when in production mode, otherwise don't do anything
                            plugins: devMode
                                ? () => []
                                : () => [
                                    postcssPresetEnv(),
                                    require('cssnano')()
                                ]
                        }
                    },
                    {
                        // Adds support for Sass files, if using Less, then
                        // use the less-loader
                        loader: 'sass-loader'
                    }
                ]
            },
            {
                // Adds support to load images in your CSS rules. It looks for
                // .png, .jpg, .jpeg and .gif
                test: /\.(png|jpe?g|gif)$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            // The image will be named with the original name and
                            // extension
                            name: '[name].[ext]',
                            // Indicates where the images are stored and will use
                            // this path when generating the CSS files.
                            // Example, in site.scss I could have
                            // url('../wwwroot/images/pattern.png') and when generating
                            // the CSS file, file-loader will output as
                            // url(../images/pattern.png), which is relative
                            // to '/css/site.css'
                            publicPath: './assets/images',
                            // When this option is 'true', the loader will emit the
                            // image to output.path. In our case we don't want this
                            emitFile: false
                        }
                    }
                ]
            },
            {
                test: require.resolve('./wwwroot/ESFAGlobals.ts'),
                use: [{
                    loader: 'expose-loader',
                    options: 'ESFA'
                }, {
                    loader: 'ts-loader'
                }],
                exclude: /node_modules/
            }
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: devMode ? './assets/dist/css/[name].css' : './assets/dist/minified/[name].min.css',
            chunkFilename: "[name].css"
        }),
        new VueLoaderPlugin()
    ],
    resolve: {
        extensions : ['.ts', '.js', '.vue', '.json'],
        alias: {
            vue$: 'vue/dist/vue.esm.js'
        }
    }
};

if (!devMode) {
    module.exports.devtool = '#source-map';
    // http://vue-loader.vuejs.org/en/workflow/production.html
    module.exports.plugins = (module.exports.plugins || []).concat([
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: '"production"'
            }
        }),
        new webpack.LoaderOptionsPlugin({
            minimize: true
        })
    ]);
}