import Vue from 'vue';
import Vuex from 'vuex';
import { IFilterItem } from '../app/Interfaces/IFilterItem';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        qualificationFilters: [] as Array<IFilterItem>,
        frameworkFilters: [] as Array<IFilterItem>,
        unitFilters: [] as Array<IFilterItem>,
        standardFilters: [] as Array<IFilterItem>,
        tlevelFilters: [] as Array<IFilterItem>
    },
    mutations: {
        updateQualificationFilters(state, filters: Array<IFilterItem>) {
            state.qualificationFilters = filters;
        },
        updateFrameworkFilters(state, filters: Array<IFilterItem>) {
            state.frameworkFilters = filters;
        },
        updateUnitFilters(state, filters: Array<IFilterItem>) {
            state.unitFilters = filters;
        },
        updateStandardFilters(state, filters: Array<IFilterItem>) {
            state.standardFilters = filters;
        },
        updateTLevelFilters(state, filters: Array<IFilterItem>) {
            state.tlevelFilters = filters;
        }
    }
});