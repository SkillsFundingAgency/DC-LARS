import Vue from 'vue';
import Vuex from 'vuex';
import { ISearchFilters } from '../app/Interfaces/ISearchFilters';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        filters : {
            awardingBodies: [],
            levels : [],
            fundingStreams : [],
            teachingYears : []
        } as ISearchFilters
    },
    mutations: {
        updateFilters(state, filters: ISearchFilters) {
            state.filters = filters;
        }
    }
});