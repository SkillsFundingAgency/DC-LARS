import Vue from 'vue';
import Vuex from 'vuex';
import { IFilterItem } from '../app/Interfaces/IFilterItem';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        filters : [] as Array<IFilterItem>
    },
    mutations: {
        updateFilters(state, filters: Array<IFilterItem>) {
            state.filters = filters;
        }
    }
});